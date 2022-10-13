using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private float knockbackDuration = 0.2f;
    private bool isKnockmackActive;
    private float knockbackStartTime;

    public override void LogicUpdate()
    {
        CheckCkonockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " damaged");
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

    private void CheckCkonockback()
    {
        if (isKnockmackActive && ((core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.CheckIfGrounded())
            || Time.time > knockbackStartTime + knockbackDuration))
        {
            isKnockmackActive=false;
            core.Movement.CanSetVelosity = true;
        }
    }
}
