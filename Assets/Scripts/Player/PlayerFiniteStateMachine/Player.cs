using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdlePlayerState { get; private set; }
    public PlayerMoveState MovePlayerState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components Variables
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    #endregion

    
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }



    private Vector2 workSpace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdlePlayerState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MovePlayerState = new PlayerMoveState(this, StateMachine, playerData, "move");
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

    public void SetVelocityX(float vel)
    {
        workSpace.Set(vel, CurrentVelocity.y);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180f, 0);
    }
}
