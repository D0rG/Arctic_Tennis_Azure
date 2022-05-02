using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Spawn : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject[] spawn;

    private int randPose;
    private int randCar;

    public float spawnRate = 2f;
    float nextSpawn = 0f;


    void Update()
    {
        if(Time.time > nextSpawn)
        {
            randPose = Random.Range(0, spawn.Length);
            randCar = Random.Range(0, cars.Length);
            Debug.Log("Spawn");
            Instantiate(cars[randCar], spawn[randPose].transform.position, spawn[randPose].transform.rotation);
            nextSpawn = Time.time + spawnRate;
        }
    }
}
