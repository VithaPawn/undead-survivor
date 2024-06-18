using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class AutoShooting : MonoBehaviour {
    #region Constants
    protected const float DELAY_BEFORE_RELEASE_BULLET = 3f;
    #endregion Constants

    #region Variables
    [Header("Auto Shooting Attributes")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected ShootingWeapon shootingWeaponSO;

    [Header("Game State Manager SO")]
    [SerializeField] protected GameStateManagerSO gameStateManagerSO;

    [Header("Time between shots / smaller = higher rate of fire")]
    protected float shootingCooldownMax;
    protected float shootingCooldown = 0f;
    [Header("Object pool of bullet")]
    protected IObjectPool<Bullet> objectPool;
    #endregion Variables

    private void Awake()
    {
        shootingCooldownMax = shootingWeaponSO.shootingBreakTime;
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, defaultCapacity: 10);
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

    private void Update()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            shootingCooldown += Time.deltaTime;
            if (shootingCooldown >= shootingCooldownMax)
            {
                Shoot();
                shootingCooldown = 0f;
            }
        }
    }

    protected virtual void Shoot() {
    }
}
