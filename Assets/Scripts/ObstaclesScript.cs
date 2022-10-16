using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var d = collision.GetComponent<IDamageable>();
        if (d != null)
        {
            d.ObstaclesDamage(damage);
        }
    }
}
