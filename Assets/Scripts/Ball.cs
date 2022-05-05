using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] protected AudioSource hitSound;
    [SerializeField] protected AudioSource missSound;
    [SerializeField][Range(0, 25)] private float factorVelocity;
    protected Rigidbody rigidbody;
    protected Vector3 lastVelosity;

    protected virtual void MakeHitSound()
    {
        hitSound.pitch = Random.Range(1f, 1.5f);
        hitSound.Play();
    }
    protected virtual void MakeMissSound()
    {
        missSound.Play();
    }

    protected virtual void VelocityFix()
    {
        if (rigidbody.velocity != Vector3.zero)
        {
            float velocityFactor = factorVelocity / rigidbody.velocity.magnitude;
            rigidbody.velocity *= velocityFactor;
        }
    }

    protected virtual void StartForce()
    {
        Vector3 vector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        rigidbody.AddForce(vector);
        lastVelosity = rigidbody.velocity;
    }
}
