using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;
    protected PlayerAttackState attackState;
    protected Core core;
    protected int attackCounter = 0;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0; 
        }

        gameObject.SetActive(true);
        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);
        gameObject.SetActive(false);
        attackCounter++;
    }

    public void InitializeWeapon(PlayerAttackState state, Core c)
    {
        attackState = state;
        core = c;
    }


    #region Animation Triggers
    
    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
    }

    public virtual void AnimationMovementStartTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationMovementStopTrigger()
    {
        attackState.SetPlayerVelocity(0);
    }


    public virtual void AnimationFlipTurnOffTrigger()
    {
        attackState.SetFlipCheck(false);
    }

    public virtual void AnimationFlipTurnOnTrigger()
    {
        attackState.SetFlipCheck(true);
    }

    public virtual void AnimationHitTrigger()
    {

    }

    #endregion

}
