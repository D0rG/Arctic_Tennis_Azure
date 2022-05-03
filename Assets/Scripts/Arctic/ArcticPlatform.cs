using UnityEngine;

public class ArcticPlatform : MonoBehaviour
{
    [SerializeField] private HandlerChestTilt chestTilt;
    [SerializeField] private float speed;
    [SerializeField] [Range(0, 10)] private float chestTiltDeathZone;
    private Transform platformTransform;
    private Rigidbody rigidbody;
    private const float rightWall = 36.36f;
    private const float leftWall = 17.9f;

    private void Awake()
    {
        platformTransform = gameObject.transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(chestTilt.angle) > chestTiltDeathZone)
        {
            Move(chestTilt.angle);
        }

#if(UNITY_EDITOR)
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            KeyboardInput();
        }
#endif
    }

    private void KeyboardInput()
    {
        float keyboardSpeed = 10f;

        if (Input.GetKey(KeyCode.A))
        {
            keyboardSpeed *= -1;
        }

        Move(keyboardSpeed);
    }

    private void Move(float direction)
    {
        float x = platformTransform.position.x;
        if ((direction > 0 && x < rightWall) || (direction < 0 && x > leftWall))
        {
            Vector3 move = new Vector3(x + direction * speed, platformTransform.position.y, 0);
            rigidbody.MovePosition(move);
        }
    }
}
