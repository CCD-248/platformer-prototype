using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapons : Weapon
{
    protected SO_SpawnWeaponData spawnWeaponData;
    [SerializeField] private Transform spawnPos;

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_SpawnWeaponData))
        {
            spawnWeaponData = (SO_SpawnWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("wrong data for the weapon");
        }
    }

    public override void AnimationHitTrigger()
    {
        base.AnimationHitTrigger();
        Debug.Log("fire arrow");
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {

        var projectile = Instantiate(spawnWeaponData.projectile, spawnPos.position, spawnPos.rotation);
        var projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(spawnWeaponData.projectileSpeed, spawnWeaponData.projectileTravelDistance, spawnWeaponData.projectileDamage);
    }
}
