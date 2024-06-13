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
    public UpgradeType UpgradeType { get { return upgradeType; } }
    public string UpgradeName { get { return upgradeName; } }
    public string Description { get { return description; } }
    public Sprite Icon { get { return icon; } }
    public int Level { get { return level; } }
    public List<UpgradeData> NextUpgrade { get { return nextUpgrade; } }
    public GameObject AbilityPrefab { get { return abilityPrefab; } }
    public float StatParameter { get { return statParameter; } }
    public int UpgradeSystemId { get { return upgradeSystemId; } }


    #endregion Methods
}