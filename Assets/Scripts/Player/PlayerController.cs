using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 debugVal;
    public int amountOfJumps = 1;
    public float airGravity = 0.5f;
    public float movementForseInAir;
    public float movementSpeed = 10f;
    public float jumpForce = 15;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform ledgeCheck;
    public float dashSpeed;
    public float dashTime;
    public float dashDistanceBetweenImages;
    public float dashCoolDown;


    public float ledgeClimbXOffset1 = 0;
    public float ledgeClimbXOffset2 = 0;
    public float ledgeClimbYOffset1 = 0;
    public float ledgeClimbYOffset2 = 0;


    public float wallHopForce;
    public float wallJumpForce;
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public float wallSlideSpeed;
    public float wallCheckRadius;
    public Transform wallCheck;
    private bool isTouchingWall;
    private bool isWallSliding;


    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    private bool isDashing;
    private bool canClimbLedge;
    private bool ledgeDetected = false;
    private bool isTouchingLedge;
    private bool isFacingRight;
    private bool isWalking;
    private bool isGrounded;
    private bool canMove = true;
    private bool canFlip = true;
    private int facingDirection = 1;
    private int amountOfJumpsLeft = 0;
    private float movementInputDirection;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;
    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    void Update()
    {
        debugVal = rb.velocity;
        CheckInput();
        CheckMovementDiraction();
        UpdateAnimations();
        CheckLedgeClimb();
        CheckIfWallSliding();
        CheckDashing();
    }

    private void FixedUpdate()
    {
        CheckSurroundings();
        ApplyMovement();
    }


    private void CheckIfWallSliding()
    {
        isWallSliding = (isTouchingWall && !isGrounded && rb.velocity.y < 0 && !canClimbLedge) ? true : false;
    }

    private void CheckLedgeClimb()
    {

        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckRadius) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckRadius) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckRadius) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckRadius) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);

            }
            animator.SetBool("canClimbLedge", canClimbLedge);

        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    public void FinishLedgeAnimation()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canFlip = true;
        canMove = true;
        ledgeDetected = false;
        animator.SetBool("canClimbLedge", canClimbLedge);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckRadius, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckRadius, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            canFlip = false;
            canMove = false;
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;

        }
        amountOfJumpsLeft = (isGrounded) ? amountOfJumps : amountOfJumpsLeft;
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDashing()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > dashDistanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canFlip = true;
                canMove = true;
            }
        }
    }

    private void ApplyMovement()
    {
        if (canMove)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
            ApplyMovementInAir();
            ApplyMovementOnWalls();
        }

    }

    private void ApplyMovementInAir()
    {
        if (!isGrounded && !isTouchingWall && movementInputDirection != 0)
        {
            var force = new Vector2(movementForseInAir * movementInputDirection, 0);
            rb.AddForce(force);
            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isTouchingWall && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airGravity, rb.velocity.y);
        }
    }

    private void ApplyMovementOnWalls()
    {
        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Jump()
    {
        if ((isGrounded || amountOfJumpsLeft > 1) && !isWallSliding)
        {
            amountOfJumpsLeft--;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        JumpOutTheWall();
    }

    private void JumpOutTheWall()
    {

        if (isWallSliding && movementInputDirection == 0 && !isGrounded)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            var force = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopDirection.y * wallHopForce);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && !isGrounded && (amountOfJumpsLeft > 1))
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            var force = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpDirection.y * wallJumpForce);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

    }

    private void CheckMovementDiraction()
    {
        if (isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }

        isWalking = (rb.velocity.x != 0) ? true : false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelosity", rb.velocity.y);
        animator.SetBool("wallSlide", isWallSliding);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(ledgePos1, ledgePos2);
    //    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    //    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckRadius, wallCheck.position.y, wallCheck.position.z));
    //    Gizmos.DrawLine(ledgeCheck.position, new Vector3(ledgeCheck.position.x + wallCheckRadius, ledgeCheck.position.y, ledgeCheck.position.z));
    //}
}
