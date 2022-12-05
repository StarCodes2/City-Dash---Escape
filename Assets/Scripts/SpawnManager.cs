using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(0, 0, 120);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerMotor playerMotorScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMotorScript = GameObject.Find("Player").GetComponent<PlayerMotor>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerMotorScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }

    }
}
