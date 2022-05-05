using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material material;
    [SerializeField] List<Color> colors; 
    private int health;

    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        InitBlock();
    }

    private void InitBlock()
    {
        int index = Random.Range(0, colors.Count);
        health = index + 1;
        material.SetColor("_EmissionColor", colors[index]);
    }

    private void AdaptColor()
    {
        material.SetColor("_EmissionColor", colors[health - 1]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            if (--health > 0)
            {
                AdaptColor();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
