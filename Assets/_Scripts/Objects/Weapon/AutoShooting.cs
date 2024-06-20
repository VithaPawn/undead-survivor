using DatabaseSystem.ScriptableObjects;
using PlayingObjects;
using UnityEngine;
using UnityEngine.Pool;

public class AutoShooting : MonoBehaviour {

    #region Variables
    [Header("Game State Manager SO")]
    [SerializeField] protected GameStateManagerSO gameStateManagerSO;

    [Header("Shooting Attributes")]
    [SerializeField] protected ShootingWeapon shootingWeaponSO;
    [SerializeField] protected Transform firePoint;

    [Header("Time between shots / smaller = higher rate of fire")]
    protected float shootingCooldownMax;
    protected float shootingCooldown = 0f;
    protected IObjectPool<Bullet> objectPool;
    #endregion Variables

    private void Awake()
    {
        shootingCooldownMax = shootingWeaponSO.GetShootingCooldown();
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledBullet, defaultCapacity: 10);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(shootingWeaponSO.GetBulletPrefab());
        bulletInstance.ObjectPool = objectPool;
        bulletInstance.Damage = shootingWeaponSO.GetBulletDamage();
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

    private void OnDestroyPooledBullet(Bullet pooledBullet)
    {
        Destroy(pooledBullet.gameObject);
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
