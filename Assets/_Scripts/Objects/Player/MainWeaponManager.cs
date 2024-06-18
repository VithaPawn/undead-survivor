using UnityEngine;

public class MainWeaponManager : MonoBehaviour, IWeaponHolder {

    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;

    [Header("Weapon Manager")]
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Weapon currentWeapon;
    private ActiveShooting shootingActive;

    [Header("Upgrade Attributes")]
    [SerializeField] private UpgradeType mainWeaponType;
    [SerializeField] private UpgradePool upgradePool;

    private void Start()
    {
        if (currentWeapon != null)
        {

            GetCurrentWeapon().SetWeaponHolder(this);
        }
    }

    private void OnEnable()
    {
        if (currentWeapon != null)
        {
            GetCurrentWeapon().gameObject.SetActive(true);
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
            GetCurrentWeapon().gameObject.SetActive(false);
        }
    }
    private void UpgradePool_OnUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData.UpgradeType == mainWeaponType)
        {
            if (!currentWeapon)
            {
                shootingActive = GetComponent<ActiveShooting>();

            }
            foreach (Transform childTransform in transform)
            {
                if (childTransform.TryGetComponent(out IUpgradeSingle upgradeSingle) &&
                    upgradeSingle.UpgradeSystemId == upgradeData.UpgradeSystemId)
                {
                    upgradeSingle.EnhanceUpgrade(upgradeData.Level);
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
