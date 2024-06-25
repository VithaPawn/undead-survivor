using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject {
    #region Variables
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private int upgradeSystemId;
    [SerializeField] private string upgradeName;
    [SerializeField] private string information;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int level;
    [SerializeField] private List<UpgradeData> nextUpgrade;
    [Header("For Weapon/Ability Type")]
    [SerializeField] private GameObject abilityPrefab;
    [Header("For Stat Type")]
    [SerializeField] private List<StatUpgradeData> statUpgrades;
    #endregion Variables

    #region Methods
    public UpgradeType UpgradeType { get { return upgradeType; } }
    public int UpgradeSystemId { get { return upgradeSystemId; } }
    public string UpgradeName { get { return upgradeName; } }
    public string Information { get { return information; } }
    public string Description { get { return description; } }
    public Sprite Icon { get { return icon; } }
    public int Level { get { return level; } }
    public List<UpgradeData> NextUpgrade { get { return nextUpgrade; } }
    public GameObject AbilityPrefab { get { return abilityPrefab; } }
    public List<StatUpgradeData> StatUpgrades { get { return statUpgrades; } }
    #endregion Methods

    #region StatData
    [Serializable]
    public class StatUpgradeData
    {
        [SerializeField] private FloatVariableSO statData;
        [SerializeField] private float additionalStatIndex;
        public FloatVariableSO StatData { get { return statData; } }
        public float AdditionalStatIndex { get { return additionalStatIndex; } }
    }
    #endregion StatData
}