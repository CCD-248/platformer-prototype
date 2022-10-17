using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform platformCheck;

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
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }


    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components Variables

    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    #endregion

    #region Unity Funcions

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();
        IdlePlayerState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MovePlayerState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpPlayerState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirPlayerState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandPlayerState = new PlayerLandState(this, StateMachine, playerData, "land");
        LedgeClimbPlayerState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledge");
        PlayerOnPlatformState = new PlayerOnPlatformState(this, StateMachine, playerData, "idle");
        DashState = new PlayerDashState(this, StateMachine, playerData, "move");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", playerData.primaryAttackCooldown);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", playerData.secondaryAttackCooldown);
    }

    private void Start()
    {
        Core.Movement.getCollisionCheck += IgnoreLayers;
        Core.Movement.SetFacingDirection(1);
        Inventory = GetComponent<PlayerInventory>();
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdlePlayerState);
    }

    private void Update()
    {
        currentState = StateMachine.CurrentState.ToString();
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Other Functions

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public Rigidbody2D GetPlatformRb()
    {
        var colider = Physics2D.OverlapCircle(Core.CollisionSenses.GroundCheck.position, playerData.groundCheckRaius, playerData.whatIsPlatform);
        var rb = colider.GetComponent<Rigidbody2D>();
        return rb;
    }
    private bool IgnoreLayers()
    {
        return true;
    }

    public float GetPlatformTransormY()
    {
        var colider = Physics2D.OverlapCircle(Core.CollisionSenses.GroundCheck.position, playerData.wallCheckRadius, playerData.whatIsPlatform);
        if (colider != null)
        {
            var rb = colider.GetComponent<Rigidbody2D>();
            return rb.transform.position.y;
        }
        colider = Physics2D.OverlapCircle(platformCheck.transform.position, playerData.wallCheckRadius, playerData.whatIsPlatform);
        if (colider != null)
        {
            var rb = colider.GetComponent<Rigidbody2D>();
            return rb.transform.position.y;
        }
        throw new NullReferenceException();
    }

    #endregion

}
