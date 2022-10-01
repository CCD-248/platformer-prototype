using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdlePlayerState { get; private set; }
    public PlayerMoveState MovePlayerState { get; private set; }
    public PlayerJumpState JumpPlayerState { get; private set; }
    public PlayerLandState LandPlayerState { get; private set; }
    public PlayerInAirState InAirPlayerState { get; private set; }


    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components Variables
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    #region Check Variables
    [SerializeField] private Transform groundCheck;

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
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
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
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }


    public bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRaius, playerData.whatIsGround);

    #endregion

    #region Other Functions

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180f, 0);
    }

    #endregion
}
