using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;
    protected AttackDetails attackDetails;
    public MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosistion, D_MeleeAttackState satateData) : base(stateMachine, entity, animBoolName, attackPosistion)
    {
        this.stateData = satateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.amountOfDamage = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;

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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
        foreach (var obj in detectedObjects)
        {
            obj.transform.SendMessage("Damage", attackDetails);
        }
    }
}
