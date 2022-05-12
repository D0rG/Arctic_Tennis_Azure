using UnityEngine;

public class ArkoPongBall : Ball
{
    private Transform ballTransform;
    private Vector3 spawnPos;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>(); 
        ballTransform = gameObject.transform;
        spawnPos = ballTransform.position;
    }

    private void Start()
    {
        GameRunner.Instance.OnStartMatch.AddListener(() => StartForce());
    }

    private void FixedUpdate()
    {
        if (lastVelosity != rigidbody.velocity)
        {
            lastVelosity = rigidbody.velocity;
            VelocityFix();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "LeftWall" || collision.gameObject.tag == "RightWall")
        {
            MakeMissSound();
            RespawnBall();
        }
        else
        {
            MakeHitSound();
        }
    }

    private void RespawnBall()
    {
        rigidbody.velocity = Vector3.zero;
        ballTransform.position = spawnPos;
        StartForce();
    }
}
