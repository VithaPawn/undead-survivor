using UnityEngine;

public class SubWeaponManager : MonoBehaviour {
    [SerializeField] private UpgradeType weaponType;
    [SerializeField] private UpgradePool upgradePool;
    [SerializeField] private GameStateManagerSO gameStateManager;

    private void OnEnable()
    {
        upgradePool.OnUpgrade += UpgradePool_OnUpgrade;
        gameStateManager.OnChanged += GameStateManager_OnChanged;
    }

    private void OnDisable()
    {
        upgradePool.OnUpgrade -= UpgradePool_OnUpgrade;
        gameStateManager.OnChanged -= GameStateManager_OnChanged;
    }

    private void UpgradePool_OnUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData.UpgradeType == weaponType)
        {
            bool isWeaponExist = false;
            foreach (Transform childTransform in transform)
            {
                if (childTransform.TryGetComponent(out IUpgradeSingle upgradeSingle) &&
                    upgradeSingle.UpgradeSystemId == upgradeData.UpgradeSystemId)
                {
                    isWeaponExist = true;
                    upgradeSingle.EnhanceUpgrade(upgradeData.Level);
                }
            }
            if (!isWeaponExist)
            {
                GameObject weaponPrefab = Instantiate(upgradeData.AbilityPrefab, transform);
                if (weaponPrefab.TryGetComponent(out IUpgradeSingle upgrade)) { upgrade.SetupUpgrade(transform); }
            }
        }
    }

    private void GameStateManager_OnChanged()
    {
        if (gameStateManager.IsGameOver())
        {
            foreach (Transform childTransform in transform)
            {
                Destroy(childTransform.gameObject);
            }
        }
    }
}
