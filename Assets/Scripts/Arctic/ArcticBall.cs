using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcticBall : Ball
{
    private Vector3 lastVelosity = Vector3.zero;

    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();    
    }

    private void FixedUpdate()
    {
        if (lastVelosity != rigidbody.velocity)
        {
            lastVelosity = rigidbody.velocity;
            VelocityFix();
        }
    }
}
