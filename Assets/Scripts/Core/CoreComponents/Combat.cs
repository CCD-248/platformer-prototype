using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool isKnockmackActive;
    private float knockbackStartTime;


    public void LogicUpdate()
    {
        CheckCkonockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " damaged");
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
        if (isKnockmackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.CheckIfGrounded())
        {
            isKnockmackActive=false;
            core.Movement.CanSetVelosity = true;
        }
    }
}
