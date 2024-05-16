using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeType", menuName = "ScriptableObjects/Upgrade/UpgradeType")]
public class UpgradeType : ScriptableObject {
    #region Variables
    [SerializeField] private string upgradeTypeName;
    [SerializeField] private string description;
    #endregion Variables
}