using DatabaseSystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortyGun : ActiveShooting, IUpgradeSingle {
    [Header("Upgrade System ID")]
    [SerializeField] private int upgradeSystemId;
    [Header("Upgrade List")]
    [SerializeField] List<ShootingWeapon> shortyGunUpgrades;

    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    public void EnhanceUpgrade(int level)
    {
        if (level <= shortyGunUpgrades.Count)
        {
            SetupShootingForWeapon(shortyGunUpgrades[level - 1]); 
        }   
    }

    public void SetupUpgrade(Transform firingPointTransform)
    {}
}
