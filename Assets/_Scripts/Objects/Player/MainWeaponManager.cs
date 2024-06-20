using UnityEngine;

public class MainWeaponManager : MonoBehaviour, IWeaponHolder {

    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;

    [Header("Weapon Manager")]
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Weapon weaponPrefab;
    private Weapon currentWeapon;

    [Header("Upgrade Attributes")]
    [SerializeField] private UpgradeType mainWeaponType;
    [SerializeField] private UpgradePool upgradePool;

    private void OnEnable()
    {
        if (currentWeapon == null)
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
        if (currentWeapon != null)
        {
            GetCurrentWeapon().Show();
        }
        playerHealth.OnChanged += PlayerHealth_OnChanged;
        upgradePool.OnUpgrade += UpgradePool_OnUpgrade;
    }


    private void OnDisable()
    {
        playerHealth.OnChanged -= PlayerHealth_OnChanged;
        upgradePool.OnUpgrade -= UpgradePool_OnUpgrade;
    }

    private void PlayerHealth_OnChanged()
    {
        if (playerHealth.GetValue() <= 0)
        {
            GetCurrentWeapon().Hide();
        }
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
                IUpgradeSingle presentUpgrade = currentWeapon.GetComponent<IUpgradeSingle>();
                if (presentUpgrade != null && presentUpgrade.UpgradeSystemId == upgradeData.UpgradeSystemId)
                {
                    presentUpgrade.EnhanceUpgrade(upgradeData.Level);
                }
            }
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
