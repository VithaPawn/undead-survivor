using UnityEngine;

public interface IUpgradeSingle {
    public int UpgradeSystemId { get; set; }

    public void EnhanceUpgrade(int level);
    public void SetupUpgrade(Transform firingPointTransform);
}
