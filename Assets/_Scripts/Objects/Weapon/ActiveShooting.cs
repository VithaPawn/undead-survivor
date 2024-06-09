using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class ActiveShooting : MonoBehaviour {
    #region Constants
    #endregion Constants

    #region Variables
    // Control shoot action objects
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private ShootingWeapon shootingWeaponSO;
    [SerializeField] private BooleanVariableSO IsPlayerTurnRight;
    [Header("Game State Manager SO")]
    [SerializeField] private GameStateManagerSO GameStateManagerSO;
    [Header("Time between shots / smaller = higher rate of fire")]
    private float shootingCooldownMax;
    private float shootingCooldown = 0f;
    private bool isBreakingTime = false;
    [Header("Object pool of bullet")]
    private IObjectPool<Bullet> objectPool;
    [Header("Sound when firing")]
    [SerializeField] private SoundChannelSO shootSound;
    private Weapon weapon;
    #endregion Variables

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        shootingCooldownMax = shootingWeaponSO.shootingBreakTime;
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, defaultCapacity: 10);
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
        bulletInstance.Damage = shootingWeaponSO.bulletDamage;
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
