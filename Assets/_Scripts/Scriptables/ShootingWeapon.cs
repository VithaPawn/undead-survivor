using UnityEngine;

namespace DatabaseSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "ShootingWeapon", menuName = "ScriptableObjects/Weapons/ShootingWeapon")]
    public class ShootingWeapon : ScriptableObject {
        [SerializeField] private int id;
        [Header("Shooting Attributes")]
        [SerializeField] private float shootingForce;
        [SerializeField] private float shootingCooldown;

        [Header("Bullet Attributes")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletDamage;
        [SerializeField] private int bulletAmountPerShot;

        [Header("Effects and Sounds")]
        [SerializeField] private SoundChannelSO shootSound;

        public float GetShootingForce() {return shootingForce;}
        public float GetShootingCooldown() {return shootingCooldown;}
        public Bullet GetBulletPrefab() {return bulletPrefab;}
        public float GetBulletDamage() {return bulletDamage;}
        public int GetBulletAmountPerShot() {return bulletAmountPerShot;}
        public SoundChannelSO GetShootSound() {return shootSound;}
        
    }
}