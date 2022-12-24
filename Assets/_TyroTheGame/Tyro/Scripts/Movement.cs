using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // ---- STATS ----
    public float speed = 18f;
    public float speedWhileOnAir = 15f;

    public bool isDead = false;

    // ---- SKILLS STATS ----
    // ----------------------
    // - JUMP
    private float lastMoveDirY = 0;
    public float jumpSpeed = 13f;
    public float secondJumpSpeed = 18f;
    private float maxJumps = 2;
    private float jumpCounter = 0;
    public float jumpButtonReleaseFactor = 0.5f;
    // - DASH
    private bool canDash = true;
    public bool isDashing = false;
    public float dashSpeed = 35f;
    public float dashUpwardsVelocity = 0.5f;
    private float dashStopTime;
    public float dashDuration = 0.4f;
    private Vector3 initialDashMoveDirection = Vector3.zero;

    // ---- GRAVITY ----
    public bool wasGrounded = false;
    public float gravity = 1.4f;
    public float fallVelocity;
    public float fallMultiplier = 1f;
    public float yUpMultiplier = 1f;
    public float yDownMultiplier = 2.5f;

    // ---- MOMENTUM ----
    public Vector3 lastMoveDirection = Vector3.zero;
    public Vector3 momentum = Vector3.zero;
    private Vector3 lastPlayerInput = Vector3.zero;
    private Vector3 playerInputRaw = Vector3.zero;
    public float momentumVelocity = 0.2f;

    // ---- SLOPES ----
    public bool isOnSlope = false;
    private Vector3 hitNormal; //Normal vector where the player is
    public float slideVelocity = 4;
    public float slideVelocityY = -6;
    private Vector3 slopeVelocity = Vector3.zero;


    // ---- PLAYER MOVEMENT ----
    private Vector3 playerInput = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    public float rotationSpeed = 8.5f;
    private Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;


    // ---- PLATFORMS ----
    public bool isOnMovingPlatform = false;
    // ---- REFERENCES ----
    private CharacterController controller;
    public GameObject playerModel;


    // ---- ANIMATION PARAMETERS ----
    private Animator anim;

    public void setDead(bool state)
    {
        this.isDead = state;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = playerModel.GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!isDead)
        {
            playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            playerInputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            playerInput = Vector3.ClampMagnitude(playerInput, 1);
            GetCamDirection();



            moveDirection = playerInput.x * camRight + playerInput.z * camForward;

            //If the player is on the grounds, it moves differently than when it is in the air.
            if (controller.isGrounded)
            {
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speedWhileOnAir;
            }

            //moveDirection.y = lastMoveDirY;

            //IF THERE'S NO INPUT
            if (moveDirection.magnitude > 0.05)
            {
                Rotate();
            }

            //Debug.Log(playerInputRaw.magnitude);
            if (playerInputRaw.magnitude > 0) anim.SetBool("run", true); else anim.SetBool("run", false);



            SetGravity();
            CheckSkills();


            //SetMomentum();
            lastMoveDirection = moveDirection;
            //Debug.Log(controller.velocity.y);
            AddForceToKeepOnGround();
            anim.SetFloat("velocityY", controller.velocity.y);
            anim.SetBool("isGrounded", controller.isGrounded);
            controller.Move(moveDirection * Time.deltaTime);

        }

        //Slide(); //if there's
    }

    private void CheckSkills()
    {
        Jump();
        Dash();
    }
    public void GetCamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    public void Rotate()
    {
        if (controller.velocity.magnitude > 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        }
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, lookRotation, rotationSpeed / 2);
    }

    public void SetGravity()
    {
        //Debug.Log(fallVelocity);
        moveDirection.y = -0.01f;

        //This is related to Gravity

        fallMultiplier = (controller.velocity.y < -0) ? yDownMultiplier : yUpMultiplier;
        if (controller.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            moveDirection.y = fallVelocity;
        }
        else
            if (!isDashing) fallVelocity += -gravity * Time.deltaTime;


        //moveDirection.y = lastMoveDirection.y + (fallVelocity * fallMultiplier);

        if (!controller.isGrounded || (!controller.isGrounded && !isOnMovingPlatform)) moveDirection.y = lastMoveDirection.y + (fallVelocity * fallMultiplier);

        //Debug.Log(isOnMovingPlatform);
        if (isOnMovingPlatform)
        {
            //moveDirection.y -= 1f;
        }
        //Debug.Log(controller.isGrounded);
        //Debug.Log("Fall Velocity: " + fallVelocity);
        //Debug.Log(moveDirection.y);
        //Debug.Log(controller.isGrounded + "after");
        //  if (!isDashing) moveDirection.y -= gravity * (fallMultiplier) * Time.deltaTime

    }

    private void AddForceToKeepOnGround() {
        if(jumpCounter == 0 && controller.isGrounded && !isDashing)
        {
            moveDirection.y -= 7f;
        }
    }
    public void SetMomentum()
    {
        /*
        frictionVelocityX = -controller.velocity.x * friction * frictionMultiplier;
        frictionVelocityZ = -controller.velocity.z * friction * frictionMultiplier;
        Debug.Log(momentum);
        */

        /*frictionVelocityX = momentum.x * frictionMultiplier;
        frictionVelocityZ = momentum.z * frictionMultiplier;

        moveDirection.x += frictionVelocityX;
        moveDirection.z += frictionVelocityZ;



    //if(controller.isGrounded && Mathf.Abs(controller.velocity.x) > 0 && Mathf.Abs(frictionVelocityX) < moveDirection.x)
    //if the player isnt grounded, if there's no input(?), if it's moving, if frictionVelocity is smaller that the moveDirection.x
    /*
    if (controller.isGrounded)
    {
        frictionVelocity = -friction * Time.deltaTime;
    }
    else
    {
        frictionVelocity -= friction * Time.deltaTime;
    }
        moveDirection.y = moveDirection.y + fallVelocity * fallMultiplier;
    /*if (!isDashing)
    {
        if (Mathf.Abs(playerInput.x) < 0.2) // if there's no input on the horizontal axis
            //moveDirection.x = Mathf.SmoothDamp(moveDirection.x, 0f, ref moveDirection.x, friction); // smoothdamp to zero
            moveDirection.x = Mathf.Lerp(moveDirection.x, 0f, friction*Time.deltaTime);
        if (Mathf.Abs(playerInput.z) < 0.2) // if there's no input on the vertical axis
            //moveDirection.z = Mathf.SmoothDamp(moveDirection.z, 0f, ref moveDirection.z, friction); // smoothdamp to zero
            moveDirection.z = Mathf.Lerp(moveDirection.z, 0f, friction* Time.deltaTime);
    }*/
    }
    public void Jump()
    {
        if (controller.isGrounded) jumpCounter = 0;
        if (Input.GetButtonDown("Jump") && jumpCounter < maxJumps)
        {

            if (jumpCounter == 0) //First Jump
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                fallVelocity = -gravity * Time.deltaTime;
                moveDirection.y = secondJumpSpeed;
            }
            lastMoveDirY = moveDirection.y; //GUARDO EN ESTA VARIABLE EL VALOR UPDATEADO DEL MOVEDIRECTION.Y POR SI NECESITO USARLA EN EL DASH
            jumpCounter++;
            anim.SetTrigger("jump");
            anim.SetBool("run", false);

            if (isDashing) isDashing = !isDashing;
        }

        //MORE RESPONSIVE JUMPS -> IF U ONLY TAP, YOU JUMP A LITTLE LESS
        if (Input.GetButtonUp("Jump") && controller.velocity.y > 0)
        {
            moveDirection.y *= jumpButtonReleaseFactor;
        }



        /*
        if (controller.velocity.y < -0) //Si está cayendo
        {
            fallMultiplier = yDownMultiplier;
        }
        else
        {
            fallMultiplier = yUpMultiplier;
        }*/
    }
    public void Dash()
    {
        if (controller.isGrounded) { canDash = true; isDashing = false; }
        //DASH
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash)
            {
                anim.SetTrigger("dash");
                anim.SetBool("isDashing", true);
                anim.SetBool("run", false);
                isDashing = true;
                canDash = false;
                //initialPosition = transform.position;
                dashStopTime = Time.time + dashDuration;


                if (Mathf.Abs(playerInput.x) > 0 || Mathf.Abs(playerInput.z) > 0)
                {
                    Vector3 aux = new Vector3(moveDirection.x, 0, moveDirection.z);
                    initialDashMoveDirection = aux.normalized;
                }
                else
                {
                    initialDashMoveDirection = camForward.normalized;
                }
            }
        }
        //MORE RESPONSIVE DASH
        /*
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashing = false;
        }
        */
        if (isDashing)
        {
            moveDirection = initialDashMoveDirection * dashSpeed;
            Rotate();
            moveDirection.y = dashUpwardsVelocity;
            if (Time.time > dashStopTime)
            {
                isDashing = false;
                anim.SetBool("isDashing", false);
            }
        }
    }
    public void Slide()
    {
        // TODO: make it work f* properly
        //Debug.Log(Vector3.Angle(Vector3.up, hitNormal));
        isOnSlope = (Vector3.Angle(Vector3.up, hitNormal) >= controller.slopeLimit && Vector3.Angle(Vector3.up, hitNormal) < 85);

        if (isOnSlope)
        {
            slopeVelocity.x = ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            slopeVelocity.z = ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            slopeVelocity.y = slideVelocityY;
            controller.Move(slopeVelocity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;

    }

}