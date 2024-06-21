using DatabaseSystem.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class CycloneScythe : AutoShooting, IUpgradeSingle {

    [Header("Cyclone Scythe Attributes")]
    [SerializeField] private int upgradeSystemId;
    [SerializeField] private Vector2[] bulletDirections;

    [Header("Upgrade List")]
    [SerializeField] List<ShootingWeapon> scytheUpgrades;

    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    public void EnhanceUpgrade(int level)
    {
        if (level <= scytheUpgrades.Count)
        {
            SetupShootingWeapon(scytheUpgrades[level - 1]);
        }
    }

    public void SetupUpgrade(Transform firingPointTransform)
    {
        firePoint = firingPointTransform;
    }

    protected override void Shoot()
    {
        if (bulletAmountPerShot == 0)
        {
            Debug.LogError("The bullet number of CycloneScythe ability is equal 0");
            return;
        }
        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            if (i < bulletDirections.Length)
            {
                ShootSingle(bulletDirections[i]);
            }
        }
    }

    private void ShootSingle(Vector2 shootingDirection)
    {
        //Get bullet from pool
        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;
        bulletObject.transform.SetPositionAndRotation(firePoint.position, Quaternion.identity);
        bulletObject.MovingForward(shootingDirection, shootingWeaponSO.GetShootingForce());
    }
}
