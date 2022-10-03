using System.Collections;
using UnityEngine;

public class ArcticBall : Ball
{
    [SerializeField] private GameObject spawnpos;
    private Transform _spawnpos;


    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        _spawnpos = spawnpos.transform;
    }

    private void Start()
    {
        GameRunner.Instance.OnStartMatch.AddListener(() => ThrowBall());
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
        gameObject.transform.position = _spawnpos.position;
        transform.parent = spawnpos.transform;
        rigidbody.isKinematic = true;
    }

    private void ThrowBall()
    {
        transform.parent = null;
        rigidbody.isKinematic = false;
        StartForce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            MakeMissSound();
            Respawn();
            ThrowBall();
        }
        else
        {
            MakeHitSound();
            //ћ€ч улетает под 45 градусов от платформы, но только вверх, из за этого могут быть проблемы с ударом м€ча о платформу с боку, по этому подн€л нижную границу выше. » иногда это выглд€ит странно.
            if (collision.gameObject.TryGetComponent(out ArcticPlatform platform))  
            {
                int dir = (rigidbody.velocity.x > 0) ? 1 : -1; // right - left
                rigidbody.velocity = Vector3.zero;
                Vector3 vector = new Vector3(dir, 1f, 0);
                rigidbody.AddForce(vector);
                lastVelosity = rigidbody.velocity;
            }
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
