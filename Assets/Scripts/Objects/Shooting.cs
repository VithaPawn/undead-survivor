using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class Shooting : MonoBehaviour {
    #region Constants
    #endregion Constants

    #region Variables
    // Control shoot action objects
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ShootingWeapon shootingWeaponSO;
    private Weapon weapon;
    private float shootingBreakTimerMax;
    private float shootingBreakTimer = 0f;
    private bool isBreakingTime = false;
    #endregion Variables

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        shootingBreakTimerMax = shootingWeaponSO.shootingBreakTime;
    }

    private void Start()
    {
        GameInput.Instance.OnShoot += GameInput_OnShoot;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnShoot -= GameInput_OnShoot;
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e)
    {
        if (weapon.GetWeaponHolder() != null)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (isBreakingTime)
        {
            return;
        }
        isBreakingTime = true;
        //Set bullet rotation
        Quaternion bulletRotation = transform.rotation;
        if (weapon.GetWeaponHolder().localScale.x == -1)
        {
            bulletRotation *= Quaternion.Euler(0f, 0f, 180f);
        }
        //Create and fire bullet
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        bullet.MovingForward(weapon, shootingWeaponSO.shootingForce);
    }

    private void Update()
    {
        if (isBreakingTime)
        {
            shootingBreakTimer += Time.deltaTime;
            if (shootingBreakTimer >= shootingBreakTimerMax)
            {
                isBreakingTime = false;
                shootingBreakTimer = 0f;
            }
        }
    }
}
