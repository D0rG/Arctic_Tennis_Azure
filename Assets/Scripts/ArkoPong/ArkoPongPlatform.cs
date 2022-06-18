using UnityEngine;

public class ArkoPongPlatform : MonoBehaviour
{
    [SerializeField] private Player playerNum;
    [SerializeField] private Directon directonType;
    [SerializeField] private float speed;
    [SerializeField] private float deathZone;
    [SerializeField] private HandlerChestTiltForAllBodies tiltHandler;

    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;

    private Vector3 upLine;
    private Vector3 downLine;
    private Vector3 directonVector = Vector3.zero;

    private bool matchStart = false;

    private void Awake()
    {
        upLine = new Vector3(transform.position.x, transform.position.y, maxZ);
        downLine = new Vector3(transform.position.x, transform.position.y, minZ);
    }

    private void Start()
    {
        GameRunner.Instance.OnStartMatch.AddListener(() => matchStart = true);
        var settings = StatrupSettings.instance.settings;
        transform.localScale *= settings.platformSize;
        speed *= settings.platformSpeed;
    }

    private void FixedUpdate()
    {
        if (!matchStart) { return; }
        float angle = tiltHandler.angles[(int)playerNum] * (int)directonType;

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

    private enum Directon
    {
        [InspectorName("Save direction")]
        Save = 1,
        [InspectorName("Inverse direcrion")]
        Inverse= -1
    }
}
