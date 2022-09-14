using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRagneAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10;
    public float projectileSpeed = 12;
    public float projectileTravelDistance = 50;
}
