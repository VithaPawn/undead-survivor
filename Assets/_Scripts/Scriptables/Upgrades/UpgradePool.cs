using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradePool", menuName = "ScriptableObjects/Upgrade/UpgradePool")]
public class UpgradePool : ScriptableObject {
    #region Variables
    [SerializeField] private List<UpgradeData> avaiablePool;
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

    public List<UpgradeData> GetRandomAvaiableUpgrades(int size)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (avaiablePool.Count < size)
        {
            size = avaiablePool.Count;
        }

        for (int i = 0; i < size; i++)
        {
            upgradeList.Add(avaiablePool[UnityEngine.Random.Range(0, avaiablePool.Count)]);
        }
        return upgradeList;
    }

    public void AddToApplyingList(UpgradeData upgrade)
    {
        applyingList.Add(upgrade);
        avaiablePool.Remove(upgrade);
        List<UpgradeData> newUpgrades = upgrade.GetNextUpgrade();
        if (newUpgrades != null && newUpgrades.Count != 0)
        {
            avaiablePool.AddRange(newUpgrades);
        }
        OnUpgrade?.Invoke(upgrade);
    }

    public void ResetAvaiablePool()
    {
        avaiablePool.AddRange(applyingList);
        applyingList.Clear();
    }
    #endregion Methods
}
