using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRepeat : MonoBehaviour
{
    public Transform road, road2, player;

    public Vector3 repeatPos;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 2;
        repeatPos = road2.position;
        repeatPos.z *= count;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.position.z >= road2.position.z && road2.position.z > road.position.z)
        {
            road.position = repeatPos;
            repeatPos.z /= count; // reset start value
            count++;
            repeatPos.z *= count;
        } else if (player.position.z >= road.position.z && road.position.z > road2.position.z)
        {
            road2.position = repeatPos;
            repeatPos.z /= count; // reset to the start value
            count++;
            repeatPos.z *= count;
        }
    }
}
