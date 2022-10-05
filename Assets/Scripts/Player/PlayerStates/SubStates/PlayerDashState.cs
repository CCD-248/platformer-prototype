using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerDashState : PlayerAbilityState
{
    private float lastImageXpos;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = player.transform.position.x;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void CheckAbilityDone() => isAbilityDone = (Time.time >= startTime + playerData.dashTime) ? true : false;

    public bool CanDash() => Time.time >= startTime + playerData.dashCooldownTime;

    public override void DoChecks()
    {
        base.DoChecks();
        CheckAbilityDone();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocityX(playerData.dashSpeed * player.FacingDirection);
        if (Mathf.Abs(player.transform.position.x - lastImageXpos) > playerData.dashDistanceBetweenImages)
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = player.transform.position.x;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
