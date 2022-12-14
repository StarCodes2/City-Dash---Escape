using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Movement
    private CharacterController controller;
   // private bool isRunning = false;
    private int laneNumberOne = 2;
   
    //Jumping
    private bool onGround = true;
    public float jumpForce = 5.0f;

    //Sliding
    //private bool isSliding = false;
    private float Timerate = 1.3f;

    // Animation
    private Animator anim;

    //Obstacle Repeat
    public bool gameOver;
    

    // Speed Modifier
    private float originalSpeed = 7.0f;
    private float speed;
    private float turnSpeed = 0.5f;
    private float speedInceaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    private Vector3 moveVector; //= Vector3.zero;

    private int desiredLane = 1; //0:Left 1:Middle 2:Right
    private int currentLane;
    public float laneDistance = 4; //the distance between two lanes
    public float gravity = -10.0f;

    //Sound
    private AudioManager audioManager;

    // Start is called before the first frame update
    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.isGameStarted || GameManager.pause)
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
            // Saving the current lane
            currentLane = desiredLane;
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = laneNumberOne;

            audioManager.Play("Move");
        }

        if (SwipeManager.swipeLeft)
        {
            // Saving the current lane
            currentLane = desiredLane;
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;

            audioManager.Play("Move");
        }

        // Calculate where we should be in he future
        Vector3 targetPosition = Vector3.zero; //transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == laneNumberOne)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        moveVector.x = (targetPosition - transform.position).x * speed;

        //transform.position = targetPosition;
    }

    private void FixedUpdate()
    {
        if (!GameManager.isGameStarted || GameManager.pause)
            return;

        // Move the Player
        controller.Move(moveVector * Time.fixedDeltaTime);

        // Rotate the character to the direction he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, turnSpeed);
        }
    }

    public void StartRunning()
    {
        //isRunning = true;
        GameManager.isGameStarted = true;
        anim.SetTrigger("StartRunning");
        //transform.Rotate(0, 0, 0);
    }

    private IEnumerator Slide()
    {
        anim.SetBool("isSliding", true);
        audioManager.Play("Slide");
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);

        yield return new WaitForSeconds(Timerate);

        anim.SetBool("isSliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }

    private void Jump()
    {
        onGround = false;
        anim.SetBool("Jump", true);
        moveVector.y = jumpForce;
        audioManager.Play("Jump");
    }

    private void Crash()
    {
        anim.SetTrigger("Death");
       // isRunning = false;
        GameManager.singleton.OnDeath();

        if (!onGround)
        {
            //
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "GameOver")
        {
            Crash();
        }

        if (hit.gameObject.tag == "Grounded")
        {
            onGround = true;
            anim.SetBool("Jump", false);
            anim.SetTrigger("StartRunning");
        }

        // Return the previous lane if the character hits the body of the bus while tring to change lane
        if (hit.gameObject.tag == "BusBody")
        {
            desiredLane = currentLane;
            audioManager.Play("Move");
            //Debug.Log("BusBody");
        }
    }

}