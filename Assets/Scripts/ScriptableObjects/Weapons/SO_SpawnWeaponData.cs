using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Spawn Weapon")]
public class SO_SpawnWeaponData : SO_WeaponData
{
    public GameObject projectile;
    public float projectileSpeed = 15f;
    public float projectileTravelDistance = 30f;
    public float projectileDamage = 15f;

    [SerializeField] private WeaponAttackDetails[] attackDetails;
    public WeaponAttackDetails[] AttackDetails
    {
        get { return attackDetails; }
        private set { attackDetails = value; }
    }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];
        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
