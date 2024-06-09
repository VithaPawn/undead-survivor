using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradePool", menuName = "ScriptableObjects/Upgrade/UpgradePool")]
public class UpgradePool : ScriptableObject {
    #region Variables
    [SerializeField] private List<UpgradeData> initialPool;
    [SerializeField] private List<UpgradeData> availablePool;
    [SerializeField] private List<UpgradeData> applyingList;
    #endregion Variables

    #region Events
    public event Action<UpgradeData> OnUpgrade;
    #endregion Events

    #region Methods
    private void OnEnable()
    {
        OnUpgrade = null;
    }

    private void OnDisable()
    {
        ResetAvailablePool();
    }

    public List<UpgradeData> GetRandomAvailableUpgrades(int size)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (availablePool.Count < size)
        {
            size = availablePool.Count;
        }

        List<UpgradeData> tempPool = new List<UpgradeData>(availablePool);

        for (int i = 0; i < size; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tempPool.Count);
            upgradeList.Add(tempPool[randomIndex]);
            tempPool.RemoveAt(randomIndex);
        }
        return upgradeList;
    }

    public void AddToApplyingList(UpgradeData upgrade)
    {
        applyingList.Add(upgrade);
        availablePool.Remove(upgrade);
        List<UpgradeData> newUpgrades = upgrade.GetNextUpgrade();
        if (newUpgrades != null && newUpgrades.Count != 0)
        {
            availablePool.AddRange(newUpgrades);
        }
        OnUpgrade?.Invoke(upgrade);
    }

    public void ResetAvailablePool()
    {
        availablePool.Clear();
        applyingList.Clear();
        availablePool.AddRange(initialPool);
    }
    #endregion Methods
}
