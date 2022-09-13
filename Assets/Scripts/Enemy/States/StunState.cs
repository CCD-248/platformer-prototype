using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStop;
    protected bool isPlayerInMinAgroRange;
    protected bool performCloseRangeAction;

    public StunState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_StunState stateData) :
        base(stateMachine, entity, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isGrounded = entity.CheckGround();  
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.stunKnockbackSpeed, stateData.stunKnockBackAngle, entity.lastDamageDirection);
        isStunTimeOver = false;
        isMovementStop = false;
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStop)
        {
            isMovementStop = true;
            entity.SetVelocity(0);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
