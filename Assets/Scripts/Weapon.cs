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
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        weaponParentPosition = new Vector2(weaponParent.position.x, weaponParent.position.y);
    }

    private void FixedUpdate()
    {
        // Get angle between weaponDirection vector and Vector2.right vector
        weaponDirection = mousePosition - weaponParentPosition;
        float angle = Mathf.Atan2(weaponDirection.y, weaponDirection.x) * Mathf.Rad2Deg;

        // Check the case when the weapon holder turn left
        if (weaponHolder.localScale.x == -1)
        {
            angle += 180f;
        }

        // Set rotation of the weapon
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Transform GetWeaponHolder() { return weaponHolder; }

    public Vector2 GetWeaponDirection() { return weaponDirection; }
}
