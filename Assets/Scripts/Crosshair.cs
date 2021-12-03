using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    private PlayerInput input;
    private InputAction aim;

    private void Awake()
    {
        input = GetComponentInParent<PlayerInput>();
        aim = input.actions["Aim"];
    }

    private void FixedUpdate()
    {
        CalculatePosition();
    }
    public void CalculatePosition()
    {
        if (aim.activeControl?.name == "position")
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(aim.ReadValue<Vector2>());
            gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        }
        else
        {
            gameObject.transform.localPosition = aim.ReadValue<Vector2>();
        }
        
    }
}
