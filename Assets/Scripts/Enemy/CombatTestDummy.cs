using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticle;
    private Animator animator;

    public void Damage(float amount)
    {
        Instantiate(hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        animator.SetTrigger("damage");
        Debug.Log($"got {amount} damage");
    }

    public void ObstaclesDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
