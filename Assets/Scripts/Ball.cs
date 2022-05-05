using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] protected AudioSource hitSound;
    [SerializeField] protected AudioSource missSound;
    [SerializeField][Range(0, 25)] private float factorVelocity;
    protected Rigidbody rigidbody;

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
}
