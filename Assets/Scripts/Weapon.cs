using UnityEngine;

public class Weapon : MonoBehaviour {
    // Control visual weapon objects
    [SerializeField] private Camera cam;
    [SerializeField] private Transform weaponParent;
    [SerializeField] private Transform weaponHolder;

    private Vector2 mousePosition;
    private Vector2 weaponParentPosition;
    private Vector2 weaponDirection;

    private void Update()
    {
        if (weaponParent != null)
        {
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            weaponParentPosition = new Vector2(weaponParent.position.x, weaponParent.position.y);
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
        if (weaponHolder.localScale.x == -1)
        {
            angle += 180f;
        }
        return angle;
    }

    private void SetWeaponRotationByAngle(float xAngle = 0f, float yAngle = 0f, float zAngle = 0f)
    {
        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
    }

    public Transform GetWeaponHolder() { return weaponHolder; }

    public Vector2 GetWeaponDirectionNormalized() { return weaponDirection.normalized; }
}
