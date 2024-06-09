using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject {
    #region Variables
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private int upgradeSystemId;
    [SerializeField] private string upgradeName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int level;
    [SerializeField] private List<UpgradeData> nextUpgrade;
    [Header("For Upgrade/Ability Type")]
    [SerializeField] private GameObject abilityPrefab;
    [Header("For Stat Type")]
    [SerializeField] private float statParameter;
    #endregion Variables

    #region Methods
    public UpgradeType GetUpgradeType() { return upgradeType; }
    public string GetUpgradeName() { return upgradeName; }
    public string GetDescription() { return description; }
    public Sprite GetIcon() { return icon; }
    public int GetLevel() { return level; }
    public List<UpgradeData> GetNextUpgrade() { return nextUpgrade; }
    public GameObject GetAbilityPrefab() { return abilityPrefab; }
    public float GetStatParameter() { return statParameter; }
    public int GetUpgradeSystemId() { return upgradeSystemId; }
    #endregion Methods
}