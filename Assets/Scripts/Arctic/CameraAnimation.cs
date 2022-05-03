using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Transform rotatorTransform;
    [SerializeField] [Range(0, 1)] private float maxHorizontalAngle = 1f;
    [SerializeField] [Range(0, 1)] private float maxVerticalAngle = 1f;
    [SerializeField] private float speed = 10f;
    private HorizontalDirection currentVerticalDirection = HorizontalDirection.Right;
    private VerticalDirection currentHorizontalDirection = VerticalDirection.Up;

    private Vector3 rightAxis;

    void Start()
    {
        rotatorTransform = gameObject.transform;
        rightAxis = rotatorTransform.right;
    }

    void FixedUpdate()
    {
        rotatorTransform.RotateAround(rotatorTransform.position, rotatorTransform.up, speed * (int)currentVerticalDirection);

        rotatorTransform.RotateAround(rotatorTransform.position, rightAxis, speed * (int)currentHorizontalDirection);

        if (rotatorTransform.rotation.y >= maxHorizontalAngle)
        {
            currentVerticalDirection = HorizontalDirection.Left;
        }
        else if(rotatorTransform.rotation.y <= -maxHorizontalAngle)
        {
            currentVerticalDirection = HorizontalDirection.Right;
        }

        if (rotatorTransform.rotation.x >= maxVerticalAngle)
        {
            currentHorizontalDirection = VerticalDirection.Down;
        }
        else if (rotatorTransform.rotation.x <= -maxVerticalAngle)
        {
            currentHorizontalDirection = VerticalDirection.Up;
        }
    }
    
    private enum HorizontalDirection
    {
        Left = -1,
        Right = 1
    }
    private enum VerticalDirection
    {
        Up = 1,
        Down = -1
    }
}
