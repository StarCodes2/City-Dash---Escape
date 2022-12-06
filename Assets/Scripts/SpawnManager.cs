using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(0, 0, 0);
    private int count = 0;
    private float spawnDistance = 20.0f;
    private int index;
    public static bool spawn = false;
    private int breakCount = 0;
    private int spawnCount = 6;
    private PlayerMotor playerMotorScript;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnObstacle(11);
        //InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawn)
        {
            if (breakCount > 4)
            {
                Instantiate(obstaclePrefab[0], spawnPos, obstaclePrefab[0].transform.rotation);
                count++;
                SpawnObstacle(spawnCount - 1);
                breakCount = 0;
            }
            else
            {
                SpawnObstacle(spawnCount);
            }
            
            spawn = false;
        }
    }

    private void SpawnObstacle(int spawnNum)
    {
        for (int i = 0; i < spawnNum; i++)
        {
            spawnPos.z = spawnDistance * count;
            index = Random.Range(1, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[index], spawnPos, obstaclePrefab[index].transform.rotation);
            count++;
        }

        breakCount++;

    }
}
