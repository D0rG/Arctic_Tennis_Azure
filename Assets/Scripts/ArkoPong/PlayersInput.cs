using UnityEngine;

public class PlayersInput : MonoBehaviour
{
    [SerializeField] private Player playerNum;
    [SerializeField] private HandlerChestTiltForAllBodies tiltHandler;
    [SerializeField] private Rigidbody rigidbody;
    private bool collisionUp;
    private bool collisionDown;

    private void FixedUpdate()
    {
        rigidbody.velocity = GetCurrentVelocity();
    }

    private Vector3 GetCurrentVelocity()
    {
        float anlge = tiltHandler.angles[(int)playerNum];

        if (anlge == float.NaN)
        {
            Debug.Log("NaN float chest tilt");
            return Vector3.zero;
        }
        else if ((anlge > 0 && !collisionUp) || (anlge < 0 && !collisionDown))
        {
            return Vector3.forward * anlge;
        }

        return Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Roof")
        {
            collisionUp = true;
        }
        else if(collision.gameObject.tag == "Floor")
        {
            collisionDown = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Roof")
        {
            collisionUp = false;
        }
        else if (collision.gameObject.tag == "Floor")
        {
            collisionDown= false;
        }
    }

    private enum Player
    {
        first = 0, 
        second = 1
    }
}
