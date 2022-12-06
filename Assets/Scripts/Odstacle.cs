using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odstacle : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.position.z - 20 > transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
