using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool jumpInputStop;
    private bool jumpInput;
    private bool isJumping;
    private bool isTouchingLedge;
    private bool isTouchingWall;
    private bool DashInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.CheckIfGrounded();
        isTouchingWall = core.CollisionSenses.CheckIsTouchingWall();
        isTouchingLedge = core.CollisionSenses.CheckIsTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbPlayerState.SetDetectedPosition(player.transform.position);
        }
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
        xInput = player.InputHandler.NormaInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        DashInput = player.InputHandler.DashInput;

        CheckJumpMul();

        if (player.InputHandler.AttackInput[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInput[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }

        else if (isGrounded && Time.time >= startTime + playerData.platformCheckTime)
        {
            stateMachine.ChangeState(player.LandPlayerState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.LedgeClimbPlayerState);
        }
        else if (DashInput && player.DashState.CanDash())
        {
            player.InputHandler.UseDashInput();
            stateMachine.ChangeState(player.DashState);
        }
        else if (jumpInput && player.JumpPlayerState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpPlayerState);
        }
        else 
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
            player.Animator.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            player.Animator.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        }
    }

    private void CheckJumpMul()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.jumpHeightMul);
                isJumping = false;
            }
            else if (core.Movement.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetIsJumping() => isJumping = true;
}
