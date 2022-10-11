using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; set => wallCheck = value; }
    public Transform LedgeCheckHorizontal { get => ledgeCheckHorizontal; set => ledgeCheckHorizontal = value; }
    public Transform LedgeCheckVertical { get => ledgeCheckVertical; set => ledgeCheckVertical = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;

    [SerializeField] private float wallCheckRadius;
    [SerializeField] private float groundCheckRaius;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlatform;

    public bool CheckIsTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckRadius, whatIsGround);

    public bool CheckIsTouchingLedgeHorizontal() => Physics2D.Raycast(ledgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckRadius, whatIsGround);

    public bool CheckIsLedgeAhead() => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckRadius, whatIsGround);

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRaius, whatIsGround) || CheckIsOnPlatform();

    public bool CheckIsOnPlatform() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRaius, whatIsPlatform);
}
