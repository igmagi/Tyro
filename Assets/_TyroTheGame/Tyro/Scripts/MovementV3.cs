using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementV3 : MonoBehaviour
{
    // ---- STATS ----
    public float speed = 18f;
    public float speedWhileOnAir = 15f;

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

    // ---- REFERENCES ----
    private CharacterController controller;
    public GameObject playerModel;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
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
        if (playerInputRaw.magnitude > 0.1)
        {
            Rotate();
        }


        SetGravity();

        CheckSkills();

        //SetMomentum();
        lastMoveDirection = moveDirection;
        controller.Move(moveDirection * Time.deltaTime);


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
    public void SetGravity()
    {

        //This is related to Gravity
        fallMultiplier = (controller.velocity.y < -0) ? yDownMultiplier : yUpMultiplier;
        if (controller.isGrounded)
            fallVelocity = -gravity * Time.deltaTime;
        else
            if (!isDashing) fallVelocity += -gravity * Time.deltaTime;


        moveDirection.y = lastMoveDirection.y + (fallVelocity * fallMultiplier);
        //  if (!isDashing) moveDirection.y -= gravity * (fallMultiplier) * Time.deltaTime
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
