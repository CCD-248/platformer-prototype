using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Entity
{
    public E2_IdleState idleState { get; private set; }
    public E2_MoveState moveState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangeAttackState rangeAttackState { get; private set; }

    [SerializeField] public D_DodgeState dodgeStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private D_RangeAttackState rangeAttackStateData;

    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private Transform rangeAttackPosition;

    public override void Awake()
    {
        base.Awake();
        dodgeState = new E2_DodgeState(stateMachine, this, "dodge", dodgeStateData, this);
        moveState = new E2_MoveState(stateMachine, this, "move", moveStateData, this);
        idleState = new E2_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E2_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E2_StunState(stateMachine, this, "stun", stunStateData, this);
        deadState = new E2_DeadState(stateMachine, this, "dead", deadStateData, this);
        rangeAttackState = new E2_RangeAttackState(stateMachine, this, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
    }

    private void Start()
    {
        Core.Combat.onStun += ChangeToStunState;
        stateMachine.Initialize(moveState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    private void ChangeToStunState()
    {
        if (stateMachine.currentState != stunState && stunState.CanStun())
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
