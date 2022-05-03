using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_move : MonoBehaviour
{

    public float speed;

    void Update()
    {
        gameObject.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        Destroy(gameObject, 15);
    }
}
