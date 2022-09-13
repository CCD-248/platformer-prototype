using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeadState : DeadState
{
    private Enemy_2 enemy;

    public E2_DeadState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_DeadState stateData, Enemy_2 en ) : 
        base(stateMachine, entity, animBoolName, stateData)
    {
        enemy = en;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
