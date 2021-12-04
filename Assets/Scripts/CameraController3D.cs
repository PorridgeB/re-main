using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController3D : MonoBehaviour
{
    // What the camera will follow
    public Transform Target;
    public float Speed = 20f;

    private float height;

    void Start()
    {
        height = transform.position.y;
    }

    void Update()
    {
        Camera.main.ResetWorldToCameraMatrix();
        
        var targetPosition = new Vector3(Target.position.x, height, Target.position.z - height);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Speed * Time.deltaTime);

        Camera.main.worldToCameraMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(-45, Vector3.right)) * Matrix4x4.Scale(new Vector3(1, Mathf.Sqrt(2), Mathf.Sqrt(2))) * Camera.main.worldToCameraMatrix;
    }
}
