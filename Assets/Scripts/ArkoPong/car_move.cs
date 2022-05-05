using UnityEngine;

public class car_move : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float ttl;

    private void Start()
    {
        Destroy(gameObject, ttl);
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
