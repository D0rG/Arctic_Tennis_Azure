using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material material;
    [SerializeField] List<Color> colors; 

    void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        material.SetColor("_EmissionColor", colors[(Random.Range(0, colors.Count - 1))]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Destroy(gameObject);
        }
    }
}
