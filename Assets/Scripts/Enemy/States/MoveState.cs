using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected bool isDetectiongWall;
    protected bool isDetectiongLedge;
    protected bool isPlayerInMinAgroRange;
    protected D_MoveState stateData;
    public MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_MoveState stateData) : base(stateMachine, entity, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectiongLedge = core.CollisionSenses.CheckIsLedgeAhead() | entity.CheckObstacles();
        isDetectiongWall = core.CollisionSenses.CheckIsTouchingWall() | entity.CheckObstacles();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(stateData.movementSpeed * core.Movement.FacingDirection);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

