using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRepeat : MonoBehaviour
{
    private float speed = 10.0f;
    private PlayerMotor playerMotorScript;
    private float leftBound = -15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerMotorScript = GameObject.Find("Player").GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMotorScript.gameOver == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.z < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
