using UnityEngine;

public class ArkoPongPlatform : MonoBehaviour
{
    [SerializeField] private Player playerNum;
    [SerializeField] private float speed;
    [SerializeField] private float deathZone;
    [SerializeField] private HandlerChestTiltForAllBodies tiltHandler;

    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;

    private Vector3 upLine;
    private Vector3 downLine;
    private Vector3 directonVector = Vector3.zero;

    private void Awake()
    {
        upLine = new Vector3(transform.position.x, transform.position.y, maxZ);
        downLine = new Vector3(transform.position.x, transform.position.y, minZ);
    }

    private void FixedUpdate()
    {
        float angle = tiltHandler.angles[(int)playerNum];

        if(Mathf.Abs(angle) < deathZone)
        {
            angle = 0;
            directonVector = transform.position;
        } 
        else if (angle > 0)
        {
            directonVector = upLine;
        }
        else if (angle < 0)
        {
            directonVector = downLine;
        }

        transform.position = Vector3.MoveTowards(transform.position, directonVector, Mathf.Abs(angle) * speed);
    }

    private enum Player
    {
        first = 0, 
        second = 1
    }
}
