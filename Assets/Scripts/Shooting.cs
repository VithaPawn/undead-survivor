using UnityEngine;

public class Shooting : MonoBehaviour {
    // Control shoot action objects
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletForce = 10f;

    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        GameInput.Instance.OnShoot += GameInput_OnShoot;
    }

    private void GameInput_OnShoot(object sender, System.EventArgs e)
    {
        if (weapon.GetWeaponHolder() != null)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Quaternion bulletRotation = transform.rotation;
        if (weapon.GetWeaponHolder().localScale.x == -1)
        {
            bulletRotation *= Quaternion.Euler(0f, 0f, 180f);
        }
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(weapon.GetWeaponDirectionNormalized() * bulletForce, ForceMode2D.Impulse);
    }
}
