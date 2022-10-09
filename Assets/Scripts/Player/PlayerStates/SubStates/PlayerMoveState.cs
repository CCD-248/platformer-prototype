using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
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
        core.Movement.CheckIfShouldFlip(xInput);
        core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
        if (xInput == 0 && isOnPlatform)
        {
            stateMachine.ChangeState(player.PlayerOnPlatformState);
        }
        else if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdlePlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
