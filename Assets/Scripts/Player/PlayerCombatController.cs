using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitboxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput;
    private bool isAttacking;
    private bool isfirstAttack;
    private float lastInputType = Mathf.NegativeInfinity;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttack();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(combatEnabled)
            {
                gotInput = true;
                lastInputType = Time.time;
            }
        }
    }

    private void CheckAttack()
    {
        if (gotInput)
        {
            gotInput = false;
            isAttacking = true;
            isfirstAttack = !isfirstAttack;
            animator.SetBool("attack_1", true);
            animator.SetBool("firstAttack", isfirstAttack);
            animator.SetBool("isAttacking", isAttacking);
        }

        if (Time.time >= lastInputType + inputTimer)
        {
            gotInput = false;
        }
    }

    public void CheckAttackHitbox()
    {
        var detectedObjects = Physics2D.OverlapCircleAll(attack1HitboxPos.position, attack1Radius, whatIsDamageable); 
        foreach(var detectedObject in detectedObjects)
        {
            detectedObject.transform.parent.SendMessage("Damage", new[] {attack1Damage, transform.position.x});
        }
    }

    public void FinishAttack1()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack_1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitboxPos.position, attack1Radius);
    }
}
