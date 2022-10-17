using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 0.5f;


    private List<IDamageable> damageables = new List<IDamageable>();

    private void Start()
    {
        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            foreach (var item in damageables.ToList())
            {
                item.ObstaclesDamage(damageAmount);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var d = collision.GetComponent<IDamageable>();
        if (d != null)
        {
            d.ObstaclesDamage(damageAmount);
            damageables.Add(d);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var d = collision.GetComponent<IDamageable>();
        if (d != null)
        {
            damageables.Remove(d);
        }
    }
}
