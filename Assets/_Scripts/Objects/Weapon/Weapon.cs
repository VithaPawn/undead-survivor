using UnityEngine;

public class Weapon : MonoBehaviour {
    // Control visual weapon objects
    [Header("Is Player Turn Right")]
    [SerializeField] private BooleanVariableSO isPlayerTurnRight;

    private Camera cam;
    private IWeaponHolder weaponHolder;
    private Vector2 mousePosition;
    private Vector2 weaponParentPosition;
    private Vector2 weaponDirection;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (weaponHolder != null)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            weaponParentPosition = new Vector2(weaponHolder.GetWeaponFollowTransform().position.x, weaponHolder.GetWeaponFollowTransform().position.y);
            float weaponAngle = GetWeaponRotaitonAngle();
            // Set rotation of the weapon
            SetWeaponRotationByAngle(0, 0, weaponAngle);
        }
    }

    private float GetWeaponRotaitonAngle()
    {
        // Get angle between weaponDirection vector and Vector2.right vector
        weaponDirection = mousePosition - weaponParentPosition;
        float angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;

        // Check the case when the weapon holder turn left
        if (!isPlayerTurnRight.GetValue())
        {
            angle += 180f;
        }
        return angle;
    }

    private void SetWeaponRotationByAngle(float xAngle = 0f, float yAngle = 0f, float zAngle = 0f)
    {
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }

    public IWeaponHolder GetWeaponHolder() { return weaponHolder; }

    public void SetWeaponHolder(IWeaponHolder newWeaponHolder)
    {
        if (weaponHolder != null)
        {
            weaponHolder.ClearCurrentWeapon();
        }
        if (newWeaponHolder.HasCurrentWeapon())
        {
            newWeaponHolder.ClearCurrentWeapon();
        }

        weaponHolder = newWeaponHolder;
        newWeaponHolder.SetCurrentWeapon(this);

        transform.parent = newWeaponHolder.GetWeaponFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public Vector2 GetWeaponDirectionNormalized() { return weaponDirection.normalized; }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
