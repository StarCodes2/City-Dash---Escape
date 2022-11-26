using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody PlayerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody>();
        PlayerRigidBody.AddForce(Vector3.up * 100);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward *Time.deltaTime * speed);
    }
}
