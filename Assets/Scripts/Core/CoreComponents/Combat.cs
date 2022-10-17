using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable, IStunable
{
    public event Action onStun;

    [SerializeField] private float obstaclesDamageInterval = 2f;
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private float knockbackDuration = 0.2f;

    private bool isKnockmackActive;

    private float knockbackStartTime;
    private float currentStunResistance;
    private float lastStunDamageTime;
    private float lastObstaclesDamageTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
        CheckIfShouldResetStun();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + "got damage");
        core.Stats.DecreaseHealth(amount);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticle);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelosity = false;
        isKnockmackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockmackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.CheckIfGrounded())
            || Time.time > knockbackStartTime + knockbackDuration))
        {
            isKnockmackActive=false;
            core.Movement.CanSetVelosity = true;
        }
    }

    private void CheckIfShouldResetStun()
    {
        if (Time.time >= lastStunDamageTime + core.Stats.StunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public void CheckIfShouldStun(float stunDamage)
    {
        currentStunResistance -= stunDamage;
        lastStunDamageTime = Time.time;
        if (currentStunResistance <= 0)
        {
            onStun?.Invoke();
        }
    }

    private void ResetStunResistance()
    {
        currentStunResistance = core.Stats.StunResistance;
    }

    public void ObstaclesDamage(float amount)
    {
        if (Time.time >= lastObstaclesDamageTime + obstaclesDamageInterval)
        {
            lastObstaclesDamageTime = Time.time;
            Damage(amount);
            Debug.Log(core.Movement.CurrentVelocity.normalized * Vector2.one);
            Knockback(new Vector2(0, 2), amount, core.Movement.FacingDirection);
        }
    }
}
