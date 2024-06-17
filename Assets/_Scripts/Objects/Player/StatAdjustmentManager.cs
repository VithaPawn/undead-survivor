using UnityEngine;

public class StatAdjustmentManager : MonoBehaviour {
    [SerializeField] private UpgradeType stat;
    [SerializeField] private UpgradePool upgradePool;

    private void OnEnable()
    {
        upgradePool.OnUpgrade += UpgradePool_OnUpgrade;
    }

    private void OnDisable()
    {
        upgradePool.OnUpgrade -= UpgradePool_OnUpgrade;
    }

    private void UpgradePool_OnUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData.UpgradeType == stat)
        {
            upgradeData.StatEntity.Increase(upgradeData.AdditionalStatIndex);
        }
    }
}
