using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool checkFlip;


    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationName) : base(player, stateMachine, playerData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormaInputX;
        
        if (checkFlip)
        {
            core.Movement.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            core.Movement.SetVelocityX(velocityToSet * core.Movement.FacingDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetWeapon(Weapon weap)
    {
        weapon = weap;
        weapon.InitializeWeapon(this, core);
    }

    public void SetPlayerVelocity(float vel)
    {
        core.Movement.SetVelocityX(vel * core.Movement.FacingDirection);
        velocityToSet = vel;
        setVelocity = true;
    }

    public void SetFlipCheck(bool b)
    {
        checkFlip = b;    
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    #endregion

}
