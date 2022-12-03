using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    private bool isRunning = false;
    private int laneNumberOne = 2;
    //Jumping
    private bool onGround = true;
    public float jumpForce = 10;
    private bool Jumping = false;

    //Sliding
    private bool isSliding = false;
    private float Timerate = 1.3f;

    // Animation
    private Animator anim;

    //Obstacle Repeat
    public bool gameOver;

    //RigidBody
    private Rigidbody PlayerRigidbody;
    public float gravityModifier;
    

    // Speed Modifier
    private float originalSpeed = 7.0f;
    private float speed;
    private float speedInceaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    private Vector3 moveVector; //= Vector3.zero;

    private int desiredLane = 1; //0:Left 1:Middle 2:Right
    public float laneDisance = 4; //the distance between two lanes
    public float gravity = -20.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
       // PlayerRigidbody = GetComponent<Rigidbody>();
        //Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameStarted && !GameManager.pause)
            return;

        if (Time.time - speedInceaseLastTick > speedIncreaseTime)
        {
            speedInceaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            // Change the modifier text
            GameManager.singleton.UpdateModifier(speed - originalSpeed);
        }

        moveVector.z = speed;

        moveVector.y += gravity * Time.deltaTime;

        if (SwipeManager.swipeDown && onGround)
        {
            StartCoroutine(Slide());
        }

        if (SwipeManager.swipeUp && onGround)
        {
            Jump();
        }

        // Gather the input on which lane we should be
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = laneNumberOne;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // Calculate where we should be in he future
        Vector3 targetPosition = Vector3.zero;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDisance;
        } else if (desiredLane == laneNumberOne)
        {
            targetPosition += Vector3.right * laneDisance;
        }
        moveVector.x = (targetPosition - transform.position).x * speed;

        //transform.position = targetPosition;
    }

    private void FixedUpdate()
    {
        // Move the Player
        controller.Move(moveVector * Time.fixedDeltaTime);
    }

    public void StartRunning()
    {
        isRunning = true;
        GameManager.isGameStarted = true;
        anim.SetTrigger("StartRunning");
        transform.Rotate(0, 0, 0);
    }

    private IEnumerator Slide()
    {
        anim.SetBool("isSliding", true);
        yield return new WaitForSeconds(Timerate);
        anim.SetBool("isSliding", false);
    }

    private void Jump()
    {
        onGround = false;
        anim.SetBool("Jump", true);
        moveVector.y = jumpForce;
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.singleton.OnDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            Crash();
        }

        if (hit.gameObject.tag == "Grounded")
        {
            onGround = true;
            anim.SetBool("Jump", false);
            anim.SetTrigger("StartRunning");
        }
    }

}