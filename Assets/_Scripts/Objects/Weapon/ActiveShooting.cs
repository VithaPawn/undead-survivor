using DatabaseSystem.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ActiveShooting : MonoBehaviour {

    #region Variables
    [Header("Game State Manager SO")]
    [SerializeField] private GameStateManagerSO GameStateManagerSO;

    [Header("Shooting Attributes")]
    [SerializeField] private ShootingWeapon shootingWeaponSO;
    [SerializeField] private BooleanVariableSO IsPlayerTurnRight;
    [SerializeField] private float burstTime = 0.1f;

    [Header("Bullet Attributes")]
    [SerializeField] private Transform firePoint;
    private IObjectPool<Bullet> objectPool;
    private float bulletDamage;
    private int bulletAmountPerShot;

    [Header("Time Between Shots / smaller = higher rate of fire")]
    private float shootingCooldownMax;
    private float shootingCooldown = 0f;
    private bool isCooldown = false;

    private Weapon weapon;
    #endregion Variables

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        SetupShootingForWeapon(shootingWeaponSO);
    }

    private void Start()
    {
        GameInput.Instance.OnShoot += GameInput_OnShoot;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnShoot -= GameInput_OnShoot;
    }

    public void SetupShootingForWeapon(ShootingWeapon weapon)
    {
        shootingCooldownMax = weapon.GetShootingCooldown();
        bulletDamage = weapon.GetBulletDamage();
        bulletAmountPerShot = weapon.GetBulletAmountPerShot();
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, defaultCapacity: 10);
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(shootingWeaponSO.GetBulletPrefab());
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
        if (isCooldown)
        {
            return;
        }
        isCooldown = true;

        float burstTimeCounter = 0f;
        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            StartCoroutine(ShootBurstly(burstTimeCounter));
            burstTimeCounter += burstTime;
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

    private void Update()
    {
        if (isCooldown)
        {
            shootingCooldown += Time.deltaTime;
            if (shootingCooldown >= shootingCooldownMax)
            {
                isCooldown = false;
                shootingCooldown = 0f;
            }
        }
    }

}
