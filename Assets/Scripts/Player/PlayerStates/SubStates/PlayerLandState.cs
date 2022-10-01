using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput != 0)
        {
            player.StateMachine.ChangeState(player.MovePlayerState);
        }
        else if (isAnimationFinished)
        {
            player.StateMachine.ChangeState(player.IdlePlayerState);
        }
    }
}
