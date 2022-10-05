using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool DashInput;
    protected bool isOnPlatform;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isOnPlatform = player.CheckIsOnPlatform();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpPlayerState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormaInputX;
        JumpInput = player.InputHandler.JumpInput;
        DashInput = player.InputHandler.DashInput;

        if (DashInput && player.DashState.CanDash())
        {
            player.InputHandler.UseDashInput();
            stateMachine.ChangeState(player.DashState);
        }
        else if (JumpInput && player.JumpPlayerState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpPlayerState);
        }
        else if (!isGrounded)
        {
            player.JumpPlayerState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
