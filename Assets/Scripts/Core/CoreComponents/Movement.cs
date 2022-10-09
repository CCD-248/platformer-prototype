using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workSpace;
    public Rigidbody2D Rigidbody { get; private set; }

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector3.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float vel)
    {
        workSpace.Set(vel, CurrentVelocity.y);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocityY(float vel)
    {
        workSpace.Set(CurrentVelocity.x, vel);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void SetVelocity(Vector2 vel)
    {
        workSpace.Set(vel.x, vel.y);
        Rigidbody.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
}
