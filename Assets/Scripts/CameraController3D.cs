using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController3D : MonoBehaviour
{
    public Transform Target;

    private float height;

    void Start()
    {
        height = transform.position.y;
    }

    void Update()
    {
        Camera.main.ResetWorldToCameraMatrix();

        transform.position = new Vector3(Target.position.x, height, Target.position.z - height);

        Camera.main.worldToCameraMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(-45, Vector3.right)) * Matrix4x4.Scale(new Vector3(1, Mathf.Sqrt(2), Mathf.Sqrt(2))) * Camera.main.worldToCameraMatrix;
    }
}
