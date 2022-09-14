using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangeAttackState : RangeAttackState
{
    private Enemy_2 enemy;
    public E2_RangeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, 
        Transform attackPosistion, D_RangeAttackState stateData, Enemy_2 enemy) : 
        base(stateMachine, entity, animBoolName, attackPosistion, stateData)
    {
        this.enemy = enemy;
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
