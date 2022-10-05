using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public string currentState;

    public PlayerDashState DashState { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdlePlayerState { get; private set; }
    public PlayerMoveState MovePlayerState { get; private set; }
    public PlayerJumpState JumpPlayerState { get; private set; }
    public PlayerLandState LandPlayerState { get; private set; }
    public PlayerInAirState InAirPlayerState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbPlayerState { get; private set; }

    public PlayerOnPlatformState PlayerOnPlatformState { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components Variables

    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    #endregion

    #region Check Variables

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    #endregion

    #region Other Variables

    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workSpace;

    #endregion

    #region Unity Funcions

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdlePlayerState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MovePlayerState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpPlayerState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirPlayerState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandPlayerState = new PlayerLandState(this, StateMachine, playerData, "land");
        LedgeClimbPlayerState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledge");
        PlayerOnPlatformState = new PlayerOnPlatformState(this, StateMachine, playerData, "idle");
        DashState = new PlayerDashState(this, StateMachine, playerData, "move");
    }

    private void Start()
    {
        FacingDirection = 1;
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdlePlayerState);
    }

    private void Update()
    {
        currentState = StateMachine.CurrentState.ToString();
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector3.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float vel)
    {
        workSpace.Set(vel, CurrentVelocity.y);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float vel)
    {
        workSpace.Set(CurrentVelocity.x, vel);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(Vector2 vel)
    {
        workSpace.Set(vel.x, vel.y);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    #endregion

    #region Check Functions

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIsTouchingWall()=> Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckRadius, playerData.whatIsGround);

    public bool CheckIsTouchingLedge() => Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckRadius, playerData.whatIsGround);

    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRaius, playerData.whatIsGround) || CheckIsOnPlatform();

    public bool CheckIsOnPlatform() => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRaius, playerData.whatIsPlatform);

    #endregion

    #region Other Functions

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180f, 0);
    }

    public Vector2 GetCornerPostion()
    {
        var xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckRadius, playerData.whatIsGround);
        var xDist = xHit.distance;
        workSpace.Set(xDist * FacingDirection, 0);
        var yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down , ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        var yDist = yHit.distance;
        workSpace.Set(wallCheck.position.x + xDist* FacingDirection,ledgeCheck.position.y - yDist);
        return workSpace;
    }

    public Rigidbody2D GetPlatformRb()
    {
        var colider = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRaius, playerData.whatIsPlatform);
        var rb = colider.GetComponent<Rigidbody2D>();
        return rb;
    }

    #endregion

}
