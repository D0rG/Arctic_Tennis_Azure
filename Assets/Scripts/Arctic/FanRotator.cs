using UnityEngine;

public class FanRotator : MonoBehaviour
{
    [SerializeField] [Range(0, 20f)] private float speed = 10;
    private Transform fanTransform;

    void Start()
    {
        fanTransform = gameObject.transform;
    }

    void FixedUpdate()
    {
        fanTransform.RotateAround(transform.position, Vector3.up, speed);
    }
}
