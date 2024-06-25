using System.Collections;
using UnityEngine;

public class VisualWeaponShooting : Shooting {

    #region Variables

    [SerializeField] private BooleanVariableSO IsPlayerTurnRight;
    [SerializeField] private float burstTime = 0.15f;

    private Weapon weapon;
    #endregion Variables

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        SetupShootingWeapon(shootingWeaponSO);
    }


    private void Update()
    {
        if (weapon.GetWeaponHolder() != null && gameStateManagerSO.IsGamePlaying())
        {
            HandleShootingCooldown(Shoot);
        }
    }

    protected override void Shoot()
    {
        float burstTimeCounter = 0f;
        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            if (weapon.gameObject.activeSelf)
            {

                StartCoroutine(ShootBurstly(burstTimeCounter));
                burstTimeCounter += burstTime;
            }
        }
    }

    private IEnumerator ShootBurstly(float time)
    {
        yield return new WaitForSeconds(time);
        ShootSingle();
    }

    private void ShootSingle()
    {
        //Set bullet rotation
        Quaternion bulletRotation = transform.rotation;
        if (!IsPlayerTurnRight.GetValue())
        {
            bulletRotation *= Quaternion.Euler(0f, 0f, 180f);
        }
        //Get bullet from pool
        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;
        // Fire bullet
        bulletObject.transform.SetPositionAndRotation(firePoint.position, bulletRotation);
        if (shootingWeaponSO.GetShootSound())
        {
            shootingWeaponSO.GetShootSound().RaiseEvent(firePoint.position);
        }
        bulletObject.MovingForward(weapon.GetWeaponDirectionNormalized(), shootingWeaponSO.GetShootingForce());
    }
}
