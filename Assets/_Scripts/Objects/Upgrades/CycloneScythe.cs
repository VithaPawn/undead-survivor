using UnityEngine;

public class CycloneScythe : AutoShooting, IUpgradeSingle {
    [Header("Cyclone Scythe Attributes")]
    [SerializeField] private int upgradeSystemId;
    [SerializeField] private int bulletNumber = 1;
    [SerializeField] private Vector2[] bulletDirections;

    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    public void EnhanceUpgrade(int level)
    {
        bulletNumber = level;
    }

    public void SetupUpgrade(Transform firingPointTransform)
    {
        firePoint = firingPointTransform;
    }

    protected override void Shoot()
    {
        if (bulletNumber == 0)
        {
            Debug.LogError("The bullet number of CycloneScythe ability is equal 0");
            return;
        }
        for (int i = 0; i < bulletNumber; i++)
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
