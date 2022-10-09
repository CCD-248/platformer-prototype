using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon= GetComponentInParent<Weapon>(); 
    }

    private void AnimationFinishedTrigger()
    {
        weapon.AnimationFinishTrigger(); 
    }

    public virtual void AnimationMovementStartTrigger()
    {
        weapon.AnimationMovementStartTrigger();
    }

    public virtual void AnimationMovementStopTrigger()
    {
        weapon.AnimationMovementStopTrigger();
    }

    public virtual void AnimationFlipTurnOffTrigger()
    {
        weapon.AnimationFlipTurnOffTrigger();
    }

    public virtual void AnimationFlipTurnOnTrigger()
    {
        weapon.AnimationFlipTurnOnTrigger();
    }
}
