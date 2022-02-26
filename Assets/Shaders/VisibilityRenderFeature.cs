using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisibilityRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public LayerMask LayerMask;
        public RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRendering;
    };

    class VisibilityRenderPass : ScriptableRenderPass
    {
        public RenderTargetHandle maskTexture = new RenderTargetHandle();
        
        private Material visibilityMaterial;
        private LayerMask layerMask;

        public VisibilityRenderPass(Material visibilityMaterial, LayerMask layerMask)
        {
            this.visibilityMaterial = visibilityMaterial;
            this.layerMask = layerMask;

            maskTexture.Init("_MaskTex");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //cmd.GetTemporaryRT(maskTexture.id, cameraTextureDescriptor);
            cmd.GetTemporaryRT(maskTexture.id, cameraTextureDescriptor.width, cameraTextureDescriptor.height, 0);

            ConfigureTarget(maskTexture.id);
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var drawingSettings = CreateDrawingSettings(new ShaderTagId("UniversalForward"), ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
            drawingSettings.overrideMaterial = visibilityMaterial;
            drawingSettings.overrideMaterialPassIndex = 0;

            var filteringSettings = new FilteringSettings(RenderQueueRange.opaque, layerMask);

            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(maskTexture.id);
        }
    }

    class BlendRenderPass : ScriptableRenderPass
    {
        private Material blendMaterial;
        private RenderTargetIdentifier cameraColorTarget;
        private RenderTargetHandle tempTexture = new RenderTargetHandle();

        public BlendRenderPass(Material blendMaterial)
        {
            this.blendMaterial = blendMaterial;

            tempTexture.Init("_TempTexture");
        }

        public void Setup(RenderTargetIdentifier cameraColorTarget)
        {
            this.cameraColorTarget = cameraColorTarget;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get("Visibility Blend");
            cmd.Clear();

            cmd.Blit(cameraColorTarget, tempTexture.Identifier(), blendMaterial, 0);
            cmd.Blit(tempTexture.Identifier(), cameraColorTarget);

            context.ExecuteCommandBuffer(cmd);

            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }
    }

    class BlurRenderPass : ScriptableRenderPass
    {
        private int blurPassses = 4;
        private Material blurMaterial;
        //private Material blitMaterial;
        private RenderTargetIdentifier blurSource;
        private RenderTargetHandle tmpBlurRT1;
        private RenderTargetHandle tmpBlurRT2;

        public BlurRenderPass(Material blurMaterial, RenderTargetIdentifier blurSource)
        {
            this.blurMaterial = blurMaterial;
            //this.blitMaterial = blitMaterial;
            this.blurSource = blurSource;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            tmpBlurRT1.Init("tmpBlurRT1");
            tmpBlurRT2.Init("tmpBlurRT2");

            //cmd.GetTemporaryRT(tmpBlurRT1.id, cameraTextureDescriptor);
            //cmd.GetTemporaryRT(tmpBlurRT2.id, cameraTextureDescriptor);

            cmd.GetTemporaryRT(tmpBlurRT1.id, cameraTextureDescriptor.width, cameraTextureDescriptor.height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32);
            cmd.GetTemporaryRT(tmpBlurRT2.id, cameraTextureDescriptor.width, cameraTextureDescriptor.height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32);

            ConfigureTarget(tmpBlurRT1.id);
            ConfigureTarget(tmpBlurRT2.id);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get();
            cmd.Clear();

            cmd.SetGlobalFloat("_Offset", 1.5f);
            cmd.Blit(blurSource, tmpBlurRT1.Identifier(), blurMaterial, 0);

            for (int i = 1; i < blurPassses - 1; i++)
            {
                cmd.SetGlobalFloat("_Offset", 0.5f + i);
                cmd.Blit(tmpBlurRT1.Identifier(), tmpBlurRT2.Identifier(), blurMaterial, 0);

                var rttmp = tmpBlurRT1;
                tmpBlurRT1 = tmpBlurRT2;
                tmpBlurRT2 = rttmp;
            }

            cmd.SetGlobalFloat("_Offset", 0.5f + blurPassses - 1);
            //cmd.Blit(tmpBlurRT1.Identifier(), blurSource, blitMaterial);
            //cmd.Blit(tmpBlurRT1.Identifier(), blurSource);
            cmd.Blit(tmpBlurRT1.Identifier(), blurSource, blurMaterial, 0);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tmpBlurRT1.id);
            cmd.ReleaseTemporaryRT(tmpBlurRT2.id);
        }
    }

    public Settings settings = new Settings();

    private VisibilityRenderPass visibilityRenderPass;
    private BlurRenderPass blurRenderPass;
    private BlendRenderPass blendRenderPass;

    public override void Create()
    {
        var visibilityMaterial = new Material(Shader.Find("Shader Graphs/VisibilityMask"));
        var blurMaterial = new Material(Shader.Find("Shader Graphs/KawaseBlur"));
        var blendMaterial = new Material(Shader.Find("Shader Graphs/VisibilityMaskBlend"));

        visibilityRenderPass = new VisibilityRenderPass(visibilityMaterial, settings.LayerMask);
        visibilityRenderPass.renderPassEvent = settings.RenderPassEvent;

        blurRenderPass = new BlurRenderPass(blurMaterial, visibilityRenderPass.maskTexture.Identifier());
        blurRenderPass.renderPassEvent = settings.RenderPassEvent;

        blendRenderPass = new BlendRenderPass(blendMaterial);
        blendRenderPass.renderPassEvent = settings.RenderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        blendRenderPass.Setup(renderer.cameraColorTarget);
        
        renderer.EnqueuePass(visibilityRenderPass);
        renderer.EnqueuePass(blurRenderPass);
        renderer.EnqueuePass(blendRenderPass);
    }
}


