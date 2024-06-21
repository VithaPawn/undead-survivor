using DatabaseSystem.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ActiveShooting : Shooting {

    #region Variables

    [SerializeField] private BooleanVariableSO IsPlayerTurnRight;
    [SerializeField] private float burstTime = 0.1f;
    private bool isCooldown = false;

    private Weapon weapon;
    #endregion Variables

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        SetupShootingWeapon(shootingWeaponSO);
        GameInput.Instance.OnShoot += GameInput_OnShoot;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnShoot -= GameInput_OnShoot;
    }

    private void Update()
    {
        if (isCooldown)
        {
            HandleShootingCooldown(() =>
            {
                isCooldown = false;
            });
        }
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e)
    {
        if (weapon.GetWeaponHolder() != null && gameStateManagerSO.IsGamePlaying())
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        if (isCooldown)
        {
            return;
        }
        isCooldown = true;

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
