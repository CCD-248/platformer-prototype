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
    protected float lastStunTimeEnd;

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
        isGrounded = core.CollisionSenses.CheckIfGrounded();  
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        lastStunTimeEnd = Time.time;
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }
        if (isGrounded && Time.time >= startTime + stateData.stunKnockbackTime)
        {
            core.Movement.SetVelocityX(0);
        }
    }

    public bool CanStun() => (Time.time >= lastStunTimeEnd + stateData.stunCooldown);
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
