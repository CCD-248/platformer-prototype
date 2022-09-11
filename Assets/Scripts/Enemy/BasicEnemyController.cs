using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead 
    }

    [SerializeField]
    private float 
        wallCheckDistance,
        groundCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration,
        lastTouchDamageTime,
        touchDamageCooldown,
        touchDamageDuration,
        touchDamage,
        touchDamageWidth,
        touchDamageHeight;
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private GameObject 
        hitParticle, 
        deathChunkParticle, 
        deathBloodParticle;
    [SerializeField]
    private Transform 
        groundCheck, 
        wallCheck,
        touchDamageCheck;
    [SerializeField]
    private LayerMask 
        whatIsGround,
        whatIsPlayer;

    private float knockbackStartTime;
    private float currentHealth;
    private bool groundDetected;
    private bool wallDetected;
    private int facingDirection = 1, damageDirection;
    private State currentState;
    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator aliveAnimator;
    private Vector2 
        movement,
        touchDamageBotLeft,
        touchDamageTopRight;


    private void Start()
    {
        currentHealth = maxHealth;
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();   
        aliveAnimator = alive.GetComponent<Animator>();
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Moving: UpdateMovingState(); break;
            case State.Knockback: UpdateKnockbackState(); break;
            case State.Dead: UpdateDeadState(); break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance,whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnimator.SetBool("knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {

        aliveAnimator.SetBool("knockback", false);
    }

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        damageDirection = (attackDetails[1] > alive.transform.position.x) ? -1 : 1;
        Instantiate(hitParticle, aliveAnimator.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (currentHealth > 0)
        {
            SwitchState(State.Knockback);
        }
        else
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), 
                touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), 
                touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft,touchDamageTopRight, whatIsPlayer);
            if (!hit)
            {
                lastTouchDamageTime = Time.time;
                hit.SendMessage("Damage", new[] { touchDamage, transform.position.x });
            }
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0f, 180f, 0f);
    }

    private void SwitchState(State state)
    {
        switch(currentState)
        {
            case State.Moving: ExitMovingState(); break;
            case State.Knockback: ExitKnockbackState(); break;
            case State.Dead: ExitDeadState(); break;
        }

        switch (state)
        {
            case State.Moving: EnterMovingState(); break;
            case State.Knockback: EnterKnockbackState(); break;
            case State.Dead: EnterDeadState(); break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

}
