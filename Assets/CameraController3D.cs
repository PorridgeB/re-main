using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.ResetWorldToCameraMatrix();

        Vector3 moveDirection = new Vector3();

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection += new Vector3(0, 0, 1);
        }

        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection += new Vector3(0, 0, -1);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection += new Vector3(-1, 0, 0);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection += new Vector3(1, 0, 0);
        }

        transform.position += moveDirection * 10f * Time.deltaTime;

        Camera.main.worldToCameraMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(-45, Vector3.right)) * Matrix4x4.Scale(new Vector3(1, Mathf.Sqrt(2), Mathf.Sqrt(2))) * Camera.main.worldToCameraMatrix;
    }
}
