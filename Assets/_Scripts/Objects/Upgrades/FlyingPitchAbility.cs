using System.Collections;
using UnityEngine;

public class FlyingPitchAbility : AutoShooting, IUpgradeSingle {
    [Header("Flying Pitchfork Attributes")]
    [SerializeField] private int upgradeSystemId;
    [SerializeField] private int bulletNumber = 1;
    [SerializeField] private float offsetFiringArea = 0.5f;
    [SerializeField] private float burstTime = 0.1f;

    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    public void EnhanceUpgrade(int level)
    {
        bulletNumber = level;
    }

    public void SetupUpgrade(Transform playerTransform)
    {
        firePoint = playerTransform;
    }

    protected override void Shoot()
    {
        if (bulletNumber == 0)
        {
            Debug.LogError("The bullet number of FlyingPitchfork ability is equal 0");
            return;
        }
        float burstTimeCounter = 0f;
        for (int i = 0; i < bulletNumber; i++)
        {
            StartCoroutine(ShootBurstly(burstTimeCounter));
            burstTimeCounter += burstTime;
        }
    }

    private IEnumerator ShootBurstly(float time)
    {
        yield return new WaitForSeconds(time);
        ShootSingle(Quaternion.Euler(0, 0, -90), Vector2.right);
        ShootSingle(Quaternion.Euler(0, 0, 90), Vector2.left);
    }

    private void ShootSingle(Quaternion bulletRotation, Vector2 shootingDirection)
    {
        //Generate random firing point
        float yFiringPoint = Random.Range(firePoint.position.y - offsetFiringArea, firePoint.position.y + offsetFiringArea);
        Vector2 firingPoint = new Vector2(firePoint.position.x, yFiringPoint);
        //Get bullet from pool
        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;
        bulletObject.transform.SetPositionAndRotation(firingPoint, bulletRotation);
        // Fire bullet
        bulletObject.MovingForward(shootingDirection, shootingWeaponSO.shootingForce);
        StartCoroutine(bulletObject.ReturnToPoolAfterDelay(DELAY_BEFORE_RELEASE_BULLET));
    }
}
