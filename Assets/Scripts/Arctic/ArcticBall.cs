using UnityEngine;

public class ArcticBall : Ball
{
    [SerializeField] private Transform spawnpos;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameRunner.Instance.OnStartMatch.AddListener(() => Respawn());
        var settings = StatrupSettings.instance.settings;
        transform.localScale *= settings.ballSize;
        factorVelocity *= settings.ballSpeed;
    }

    private void FixedUpdate()
    {
        if (lastVelosity != rigidbody.velocity)
        {
            lastVelosity = rigidbody.velocity;
            VelocityFix();
        }
    }

    private void Respawn()
    {
        rigidbody.velocity = Vector3.zero;
        gameObject.transform.position = spawnpos.position;
        StartForce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            MakeMissSound();
            Respawn();
        }
        else
        {
            MakeHitSound();
        }
    }

    protected override void StartForce()
    {
        Vector3 vector = new Vector3(Random.Range(-0.5f, 0.5f), 1f, 0);
        rigidbody.AddForce(vector);
        lastVelosity = rigidbody.velocity;
    }

    private void OnDestroy()
    {
        GameRunner.Instance.OnStartMatch.RemoveListener(() => Respawn());
    }
}
