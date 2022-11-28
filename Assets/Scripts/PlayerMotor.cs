using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    private bool isRunning = false;

    //Jumping
    public bool onGround = true;
    public float jumpForce = 10;
    private bool Jumping = false;

    // Animation
    private Animator anim;

    //RigidBody
    private Rigidbody PlayerRigidbody;
    public float gravityModifier;
    

    // Speed Modifier
    private float originalSpeed = 7.0f;
    private float speed;
    private float speedInceaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
            return;
        if (!Jumping)
            return;

        if (Time.time - speedInceaseLastTick > speedIncreaseTime)
        {
            speedInceaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            // Change the modifier text
            GameManager.singleton.UpdateModifier(speed - originalSpeed);
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.z = speed;

        // Move the Player
        controller.Move(moveVector * Time.deltaTime);

        // Player to Jump
        if(Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            PlayerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            StartJumping();
        }

    }
    public void StartJumping()
    {
        Jumping = true;
        anim.SetTrigger("StartJumping");
        transform.Translate(0, 50, 0);
    }
    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
        transform.Rotate(0, 0, 0);
    }

}