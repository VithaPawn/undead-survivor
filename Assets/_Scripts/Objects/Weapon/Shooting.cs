using DatabaseSystem.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour {

    #region Variables
    [Header("Game State Manager SO")]
    [SerializeField] protected GameStateManagerSO gameStateManagerSO;

    [Header("Shooting Attributes")]
    [SerializeField] protected ShootingWeapon shootingWeaponSO;
    [SerializeField] protected Transform firePoint;
    protected int bulletAmountPerShot;
    protected float shootingCooldownMax;
    protected float shootingCooldown = 0f;

    protected IObjectPool<Bullet> objectPool;
    #endregion Variables

    protected virtual Bullet CreateBullet()
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

    protected virtual void SetupShootingWeapon(ShootingWeapon weapon)
    {
        shootingWeaponSO = weapon;
        shootingCooldownMax = shootingWeaponSO.GetShootingCooldown();
        bulletAmountPerShot = shootingWeaponSO.GetBulletAmountPerShot();
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, defaultCapacity: 10);
    }

    protected virtual void Shoot() { }

    protected virtual void HandleShootingCooldown(Action OnResetCooldown)
    {
        shootingCooldown += Time.deltaTime;
        if (shootingCooldown >= shootingCooldownMax)
        {
            shootingCooldown = 0f;
            OnResetCooldown();
        }
    }
}
