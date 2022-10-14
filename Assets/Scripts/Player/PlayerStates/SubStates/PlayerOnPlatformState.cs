using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerOnPlatformState : PlayerGroundedState
{
    private Rigidbody2D platformRb;
    private Vector2 workspace;

    public PlayerOnPlatformState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter platform");
        platformRb = player.GetPlatformRb();
        core.Movement.SetVelocity(platformRb.velocity);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("exit platform");
        platformRb = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.MovePlayerState);
        }
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (platformRb != null)
        {
            core.Movement.SetVelocity(platformRb.velocity);
        }
    }
}
