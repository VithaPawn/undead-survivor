using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class ActiveShooting : MonoBehaviour {
    #region Constants
    protected const float DELAY_BEFORE_RELEASE_BULLET = 3f;
    #endregion Constants

    #region Variables
    [Header("Game State Manager SO")]
    [SerializeField] private GameStateManagerSO GameStateManagerSO;

    [Header("Shooting Attributes")]
    [SerializeField] private BooleanVariableSO IsPlayerTurnRight;
    [SerializeField] private SoundChannelSO shootSound;
    [SerializeField] private ShootingWeapon shootingWeaponSO;

    [Header("Bullet Attributes")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    private IObjectPool<Bullet> objectPool;

    [Header("Time Between Shots / smaller = higher rate of fire")]
    private float shootingCooldown = 0f;
    private bool isBreakingTime = false;

    [Header("Upgrade Attributes")]
    [SerializeField] private bool isUpgraded = false;
    [SerializeField] private float shootingCooldownMax;
    [SerializeField] private float bulletDamage;
    [SerializeField] private int bulletAmountPerShot;

    private Weapon weapon;
    #endregion Variables

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, defaultCapacity: 10);
        if (!isUpgraded)
        {
            shootingCooldownMax = shootingWeaponSO.shootingBreakTime;
            bulletDamage = shootingWeaponSO.bulletDamage;
            bulletAmountPerShot = 1;
        }
    }

    private void Start()
    {
        GameInput.Instance.OnShoot += GameInput_OnShoot;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnShoot -= GameInput_OnShoot;
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.ObjectPool = objectPool;
        bulletInstance.Damage = bulletDamage;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet pooledBullet)
    {
        pooledBullet.gameObject.SetActive(false);
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e)
    {
        if (weapon.GetWeaponHolder() != null && GameStateManagerSO.IsGamePlaying())
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
        if (!IsPlayerTurnRight.GetValue())
        {
            bulletRotation *= Quaternion.Euler(0f, 0f, 180f);
        }
        //Get bullet from pool
        Bullet bulletObject = objectPool.Get();
        if (bulletObject == null) return;
        bulletObject.transform.SetPositionAndRotation(firePoint.position, bulletRotation);
        // Fire bullet
        shootSound.RaiseEvent(firePoint.position);
        bulletObject.MovingForward(weapon.GetWeaponDirectionNormalized(), shootingWeaponSO.shootingForce);
    }

    private void Update()
    {
        if (isBreakingTime)
        {
            shootingCooldown += Time.deltaTime;
            if (shootingCooldown >= shootingCooldownMax)
            {
                isBreakingTime = false;
                shootingCooldown = 0f;
            }
        }
    }

}
