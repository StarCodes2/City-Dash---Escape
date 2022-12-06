using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    private void Update()
    {
        if (player.transform.position.z - 20 > transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
