using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType
{
    Follow,
    SlowFollow,
    Input
}

public enum BindingType
{
    None,
    Sprite,
    Values
}

public class CameraController : MonoBehaviour
{
    public static CameraController _instance;

    private Camera targetCamera;

    //This is what the camera will follow
    public Transform target;

    //If the camera is bound to the dimensions of a sprite
    public SpriteRenderer background;
    //If the camera is bound to the min/max dimensions
    public Vector2 min;
    public Vector2 max;

    //Speed for input control type
    public Vector2 speed = new Vector2(5,5);
    //Speed for slow follow control type
    public float minSpeed = 0.5f;
    public float maxSpeed = 2f;

    private float ySpeed;
    private float xSpeed;

    //Creates a padding within the camera, once the player moves this far from the camera in any direction, start accelerating the speed of the camera
    private float limitY = 2f;
    private float limitX = 2f;

    private Vector2 smoothedPosition;
    public Vector2 cameraDelta;

    //Orthographic size
    public float size = 10;
    private float lastSize;
    private Vector2 cameraExtent;

    public ControlType controlType;
    public BindingType bindingType;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            targetCamera = GetComponent<Camera>();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        xSpeed = ySpeed = speed.x;
        Screen.SetResolution(45, 56, true);
    }

    // Update is called once per frame
    void Update()
    {
        size = Mathf.Clamp(size + -10 * Input.GetAxisRaw("Mouse ScrollWheel"), 2, 15);
        targetCamera.orthographicSize = size;
        cameraExtent = new Vector2(size * Screen.width / Screen.height, size);

        //Determine new position based on control type
        Vector2 newPos = Vector2.zero;
        switch (controlType)
        {
            case ControlType.Follow:
                newPos = target.transform.position;
                break;
            case ControlType.SlowFollow:
                newPos = PlayerFollow();
                break;
            case ControlType.Input:
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal") * size/100 * speed.x, Input.GetAxisRaw("Vertical") * size/100 * speed.y);
                newPos = (Vector2)transform.position + input * speed;
                break;
        }

        //Get the difference between current and new position
        cameraDelta = newPos - (Vector2)transform.position;

        //Restrict new position based on binding type
        switch (bindingType)
        {
            case BindingType.Sprite:
                smoothedPosition = ClampCameraPosition((Vector2)background.transform.position + new Vector2(-background.size.x/2, -background.size.y/2), (Vector2)background.transform.position + new Vector2(background.size.x/2, background.size.y/2));
                break;
            case BindingType.Values:
                smoothedPosition = ClampCameraPosition(min, max);
                break;
            case BindingType.None:
                smoothedPosition = newPos;
                break;

        }

        //Update position
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }

    private void LateUpdate()
    {
        //save the position and size to be referenced in the next frame
        lastSize = size;
    }

    //Simple check to see if a number is between two numbers
    private bool OutOfRange(float negativeLimit, float positiveLimit, float point)
    {
        if (point < negativeLimit || point > positiveLimit)
        {
            return true;
        }
        return false;
    }

    //Check that a vector2 is between two vector2s (on both axes)
    private bool OutOfBounds(Vector2 point, Vector2 min, Vector2 max)
    {
        if (point.x > max.x || point.x < min.x)
        {
            return true;
        }
        if (point.y > max.y || point.y < min.y)
        {
            return true;
        }
        return false;
    }

    //Reduce the speed factor until it reaches minSpeed
    private float DampSpeed(float speed)
    {
        if (speed > minSpeed)
        {
            return speed *= 0.97f;
        }
        else
        {
            return speed = maxSpeed;
        }
    }

    //Checks the camera delta and returns a new position within the bounds
    private Vector2 ClampCameraPosition(Vector2 min, Vector2 max)
    {
        var newPos = new Vector3(Mathf.Clamp(transform.position.x + cameraDelta.x, min.x + cameraExtent.x, max.x - cameraExtent.x), Mathf.Clamp(transform.position.y + cameraDelta.y, min.y + cameraExtent.y, max.y - cameraExtent.y), -10);

        var t = Time.fixedDeltaTime * speed.x;
        if (lastSize < size && OutOfBounds(new Vector2(transform.position.x + cameraDelta.x, transform.position.y + cameraDelta.y), new Vector2(min.x + cameraExtent.x, min.y + cameraExtent.y), new Vector2(max.x - cameraExtent.x, max.y - cameraExtent.y)))
        {
            t = 1;
        }
        var newPosLerped = Vector3.Lerp(transform.position, newPos, t);
        cameraDelta = new Vector2(newPosLerped.x - transform.position.x, newPosLerped.y - transform.position.y);
        return newPosLerped;
    }

    //Returns a position moving towards the player, accelerates at larger distances
    private Vector2 PlayerFollow()
    {
        PlayerFollowY();
        PlayerFollowX();

        var timeFactor = 0f;
        if (Vector3.Distance(transform.position, target.position) > 7.5)
        {
            timeFactor = Time.deltaTime;
        }
        else
        {
            timeFactor = 0.03f;
        }

        return new Vector2(Mathf.Lerp(transform.position.x, target.position.x, timeFactor * xSpeed), Mathf.Lerp(transform.position.y, target.position.y, timeFactor * ySpeed));
    }

    //Changes the speed factor on the y-axis at a distance threshold
    private void PlayerFollowY()
    {
        if (OutOfRange(transform.position.y - limitY, transform.position.y + limitY, target.transform.position.y))
        {
            ySpeed *= 1.005f;
        }
        else
        {
            ySpeed = DampSpeed(ySpeed);
        }
    }

    //Changes the speed factor on the x-axis at a distance threshold
    private void PlayerFollowX()
    {
        if (OutOfRange(transform.position.x - limitX, transform.position.x + limitX, target.transform.position.x))
        {
            xSpeed *= 1.005f;
        }
        else
        {
            xSpeed = DampSpeed(xSpeed);
        }
    }

    //Updates the camera's tracking target
    public void ChangeTarget(Transform newTarget, float distance)
    {
        target = newTarget;
        size = distance;
    }

    //relocate camera to target instantly
    public void JumpToTarget()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10);
            targetCamera.orthographicSize = size;
        }
    }

    
}
