using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour, IWeaponHolder {

    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;
    [Header("Weapon Manager")]
    [SerializeField] private Transform weaponHoldPoint;
    [SerializeField] private Weapon currentWeapon;

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
    }

    private void OnDisable()
    {
        playerHealth.OnChanged -= PlayerHealth_OnChanged;
    }

    private void PlayerHealth_OnChanged()
    {
        if (playerHealth.GetValue() <= 0)
        {
            GetCurrentWeapon().gameObject.SetActive(false);
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
