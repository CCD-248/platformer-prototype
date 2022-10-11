using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPosition;
    private Vector2 cornerPosition;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workSpace;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Animator.SetBool("ledge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityZero();
        player.transform.position = detectedPosition;
        cornerPosition = GetCornerPostion();
        startPos.Set(cornerPosition.x - (core.Movement.FacingDirection * playerData.startOffset.x), cornerPosition.y - playerData.startOffset.y);
        stopPos.Set(cornerPosition.x + (core.Movement.FacingDirection * playerData.stopOffset.x), cornerPosition.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.position = stopPos;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            player.StateMachine.ChangeState(player.IdlePlayerState);
        }
        else
        {
            core.Movement.SetVelocityZero();
            player.transform.position = startPos;
        }
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPosition = pos;

    private Vector2 GetCornerPostion()
    {
        var xHit = Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, playerData.wallCheckRadius, playerData.whatIsGround);
        var xDist = xHit.distance;
        workSpace.Set(xDist * core.Movement.FacingDirection, 0);
        var yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workSpace), Vector2.down, core.CollisionSenses.LedgeCheckHorizontal.position.y - core.CollisionSenses.WallCheck.position.y, playerData.whatIsGround);
        var yDist = yHit.distance;
        workSpace.Set(core.CollisionSenses.WallCheck.position.x + xDist * core.Movement.FacingDirection, core.CollisionSenses.LedgeCheckHorizontal.position.y - yDist);
        return workSpace;
    }
}
