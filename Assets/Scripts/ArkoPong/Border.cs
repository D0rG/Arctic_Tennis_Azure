using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private BorderType border;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ball")
        {
            if (border == BorderType.left)
            {
                Score.instance.AddLeft();
            }
            else
            {
                Score.instance.AddRight();
            }
        }
    }

    public enum BorderType
    {
        left, 
        right
    }
}
