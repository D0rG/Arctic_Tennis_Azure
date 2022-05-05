using UnityEngine;

public class Car_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    [SerializeField] private Transform[] spawn;

    private int randPose;

    public float spawnRate = 2f;
    float nextSpawn = 0f;


    void Update()
    {
        if(Time.time > nextSpawn)
        {
            randPose = Random.Range(0, spawn.Length);
            Instantiate(cars[Random.Range(0, cars.Length)], spawn[randPose].position, spawn[randPose].rotation);
            nextSpawn = Time.time + spawnRate;
        }
    }
}
