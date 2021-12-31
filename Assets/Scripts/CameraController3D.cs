using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController3D : MonoBehaviour
{
    // What the camera will follow
    public Transform Target;
    public Vector2 Offset = new Vector2(0, 0.5f);
    public float Speed = 20f;
    public int PixelsPerUnit = 16;
    public bool Smooth = false;
    public bool SnapToNearestPixel = true;

    private float height;

    void Start()
    {
        Target = GameObject.Find("Player").transform;
        height = transform.position.y;
        var camera = GetComponent<Camera>();
        camera.orthographicSize = (camera.targetTexture.height / 2f) / PixelsPerUnit;
    }

    void Update()
    {
        Camera.main.ResetWorldToCameraMatrix();
        
        var targetPosition = new Vector3(Target.position.x + Offset.x, height, Target.position.z + Offset.y - height);

        if (Smooth)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Speed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (SnapToNearestPixel)
        {
            var nearestX = Mathf.Round(transform.position.x * PixelsPerUnit) / PixelsPerUnit;
            var nearestY = Mathf.Round(transform.position.y * PixelsPerUnit) / PixelsPerUnit;
            var nearestZ = Mathf.Round(transform.position.z * PixelsPerUnit) / PixelsPerUnit;

            transform.position = new Vector3(nearestX, nearestY, nearestZ);
        }

        Camera.main.worldToCameraMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(-45, Vector3.right)) * Matrix4x4.Scale(new Vector3(1, Mathf.Sqrt(2), Mathf.Sqrt(2))) * Camera.main.worldToCameraMatrix;
    }
}
