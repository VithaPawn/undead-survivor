using DatabaseSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShortyGun : VisualWeaponShooting, IUpgradeSingle {
    [Header("Default Weapon Data")]
    private ShootingWeapon defaultWeaponData;
    [Header("Upgrade System")]
    [SerializeField] private int upgradeSystemId;
    [SerializeField] List<ShootingWeapon> shortyGunUpgrades;

    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    private void Awake()
    {
        defaultWeaponData = shootingWeaponSO;
    }

    public void EnhanceUpgrade(int level)
    {
        if (level <= shortyGunUpgrades.Count)
        {
            SetupShootingWeapon(shortyGunUpgrades[level - 1]); 
        }   
    }

    public void SetupUpgrade(Transform firingPointTransform)
    {
        if (defaultWeaponData)
        {
            shootingWeaponSO = defaultWeaponData;
        }
        else
        {
            Debug.LogError("There is no default weapon data");
        }
    }
}
