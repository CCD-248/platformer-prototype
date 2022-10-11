using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;

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
            var damageable = obj.GetComponent<IDamageable>();
            if (damageable != null)
            { 
                damageable.Damage(stateData.attackDamage);
            }
            var knockbackable = obj.GetComponent<IKnockbackable>();
            if (knockbackable != null)
            {
                knockbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, core.Movement.FacingDirection);
            }
        }
    }
}
