using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public event Func<bool> getCollisionCheck;
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workSpace;
    public bool CanSetVelosity { get; set; }    
    public Rigidbody2D Rigidbody { get; private set; }
    public int FacingDirection { get; private set; }

    private bool ignoreLayers;

    protected override void Awake()
    {
        base.Awake();
        FacingDirection = 1;
        CanSetVelosity = true;
        Rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
        if (getCollisionCheck != null)
        {
            ignoreLayers = getCollisionCheck.Invoke();
        }
    }


    public void SetVelocityZero()
    {
        workSpace = Vector3.zero;
        SetFinalVelocity();
    }

    public void SetVelocityX(float vel)
    {
        workSpace.Set(vel, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float vel)
    {
        workSpace.Set(CurrentVelocity.x, vel);
        SetFinalVelocity();
    }

    public void SetVelocity(Vector2 vel)
    {
        workSpace.Set(vel.x, vel.y);
        SetFinalVelocity();
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0, 180f, 0);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void SetFacingDirection(int val)
    {
        FacingDirection = val;
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelosity)
        {
            Rigidbody.velocity = workSpace;
            CurrentVelocity = workSpace;
        }
    }
}
