using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    public float smoothSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayer();
        EnemyFollow();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        newPosition.y = 3;
        transform.position = newPosition;
    }

    public void AssignPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().transform;
        offset = transform.position - target.position;
    }

    public void EnemyFollow()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyChasing>().transform;
        offset = transform.position - target.position;
    }
}
