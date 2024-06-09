using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour {
    [SerializeField] private UpgradeType ability;
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
        if (upgradeData.GetUpgradeType() == ability)
        {
            foreach (Transform childTransform in transform)
            {
                if (childTransform.TryGetComponent(out IUpgradeSingle abilityBase))
                {
                    if (abilityBase.UpgradeSystemId == upgradeData.GetUpgradeSystemId())
                    {
                        Destroy(childTransform.gameObject);
                    }
                }
            }
            GameObject abilityPrefab = Instantiate(upgradeData.GetAbilityPrefab(), transform);
            if (abilityPrefab.TryGetComponent(out IUpgradeSingle abilitySingle)) { abilitySingle.SetupUpgrade(transform); }
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
