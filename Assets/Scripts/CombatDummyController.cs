using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField]
    private bool applyKnockBack;

    private float currentHealth;
    private float knockbackStart;
    private int playerFacingDirection;
    private bool isFacingLeft, knockback;

    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D aliveRB, brokenTopRB, brokenBotRB;
    private Animator animatorAlive;


    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Dummy top").gameObject;
        brokenBotGO = transform.Find("Dummy botton").gameObject;

        animatorAlive = aliveGO.GetComponent<Animator>();
        aliveRB = aliveGO.GetComponent<Rigidbody2D>();
        brokenTopRB = brokenTopGO.GetComponent<Rigidbody2D>();
        brokenBotRB = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenBotGO.SetActive(false);
        brokenTopGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log("taken damage");
        currentHealth -= amount;
        playerFacingDirection = pc.GetFacingDirection();

        isFacingLeft = (playerFacingDirection == 1) ? true : false;
        animatorAlive.SetBool("playerOnLeft", isFacingLeft);
        animatorAlive.SetTrigger("damage");

        if (applyKnockBack && currentHealth > 0f)
        {
            Knockback();
        }
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        aliveRB.velocity = new Vector2(playerFacingDirection * knockbackSpeedX, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration)
        {
            knockback = false;
            aliveRB.velocity = new Vector2(0f, aliveRB.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenBotGO.SetActive(true);
        brokenTopGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        brokenBotRB.velocity = new Vector2(playerFacingDirection * knockbackSpeedX, knockbackSpeedY);
        brokenTopRB.velocity = new Vector2(playerFacingDirection * knockbackDeathSpeedX, knockbackDeathSpeedY);
        brokenTopRB.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }

}
