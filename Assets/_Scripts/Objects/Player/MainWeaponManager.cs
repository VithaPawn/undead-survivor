using UnityEngine;

public class MainWeaponManager : MonoBehaviour, IWeaponHolder {

    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;

    [Header("Weapon Manager")]
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Weapon weaponPrefab;
    private Weapon currentWeapon;

    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManager;

    [Header("Upgrade Attributes")]
    [SerializeField] private UpgradeType mainWeaponType;
    [SerializeField] private UpgradePool upgradePool;

    private void OnEnable()
    {
        if (!HasCurrentWeapon())
        {
            if (weaponHoldPoint.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.SetWeaponHolder(this);
            }
            else
            {
                Weapon newWeapon = Instantiate(weaponPrefab);
                newWeapon.SetWeaponHolder(this);
            }
        }
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
        if (upgradeData.UpgradeType == mainWeaponType && upgradeData.AbilityPrefab.TryGetComponent(out Weapon weapon))
        {
            if (!HasCurrentWeapon())
            {
                weapon.SetWeaponHolder(this);
                return;
            }
            if (HasCurrentWeapon())
            {
                IUpgradeSingle currentUpgrade = currentWeapon.GetComponent<IUpgradeSingle>();
                if (currentUpgrade != null && currentUpgrade.UpgradeSystemId == upgradeData.UpgradeSystemId)
                {
                    currentUpgrade.EnhanceUpgrade(upgradeData.Level);
                }
            }
        }
    }

    private void GameStateManager_OnChanged()
    {
        if (gameStateManager.IsGamePlaying())
        {
            IUpgradeSingle currentUpgrade = GetCurrentWeapon().GetComponent<IUpgradeSingle>();
            currentUpgrade.SetupUpgrade(transform);
        }
    }

    public void ClearCurrentWeapon()
    {
        currentWeapon = null;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public Transform GetWeaponFollowTransform()
    {
        return weaponHoldPoint;
    }

    public Transform GetWeaponHolderTransform()
    {
        return transform;
    }

    public bool HasCurrentWeapon()
    {
        return (currentWeapon != null);
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
