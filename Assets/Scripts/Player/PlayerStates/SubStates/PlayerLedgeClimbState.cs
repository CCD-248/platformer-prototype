using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPosition;
    private Vector2 cornerPosition;
    private Vector2 startPos;
    private Vector2 stopPos;

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
        player.SetVelocityZero();
        player.transform.position = detectedPosition;
        cornerPosition = player.GetCornerPostion();
        startPos.Set(cornerPosition.x - (player.FacingDirection * playerData.startOffset.x), cornerPosition.y - playerData.startOffset.y);
        stopPos.Set(cornerPosition.x + (player.FacingDirection * playerData.stopOffset.x), cornerPosition.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("exit climb " + stopPos.ToString());
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
            player.SetVelocityZero();
            player.transform.position = startPos;
        }
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPosition = pos;
}
