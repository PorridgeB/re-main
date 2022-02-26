using UnityEngine;
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
        private Material visibilityMaterial;
        private LayerMask layerMask;
        private RenderTargetHandle maskTexture = new RenderTargetHandle();

        public VisibilityRenderPass(Material visibilityMaterial, LayerMask layerMask)
        {
            this.visibilityMaterial = visibilityMaterial;
            this.layerMask = layerMask;

            maskTexture.Init("_MaskTex");
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(maskTexture.id, cameraTextureDescriptor);

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

    public Settings settings = new Settings();

    private VisibilityRenderPass visRenderPass;
    private BlendRenderPass blendRenderPass;

    public override void Create()
    {
        var visibilityMaterial = new Material(Shader.Find("Shader Graphs/VisibilityMask"));
        var blendMaterial = new Material(Shader.Find("Shader Graphs/VisibilityMaskBlend"));

        visRenderPass = new VisibilityRenderPass(visibilityMaterial, settings.LayerMask);
        visRenderPass.renderPassEvent = settings.RenderPassEvent;

        blendRenderPass = new BlendRenderPass(blendMaterial);
        blendRenderPass.renderPassEvent = settings.RenderPassEvent;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        blendRenderPass.Setup(renderer.cameraColorTarget);
        
        renderer.EnqueuePass(visRenderPass);
        renderer.EnqueuePass(blendRenderPass);
    }
}


