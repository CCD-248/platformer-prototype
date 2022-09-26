using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;

    private string animationBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        animationBoolName = animationName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Animator.SetBool(animationBoolName, true);
        startTime = Time.time;
    }

    public virtual void Exit()
    {
        DoChecks();
        player.Animator.SetBool(animationBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
