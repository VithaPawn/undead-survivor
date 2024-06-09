using UnityEngine;

public class ShovelProtectorAbility : MonoBehaviour, IUpgradeSingle {
    [SerializeField] private int upgradeSystemId;
    public int UpgradeSystemId { get => upgradeSystemId; set => upgradeSystemId = value; }

    public void EnhanceUpgrade(int level) { }

    public void SetupUpgrade(Transform playerTransform)
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.TryGetComponent(out SingleShovelProtector shovel))
            {
                shovel.SetRotateCenterTransform(playerTransform);
            }
        }
    }
}
