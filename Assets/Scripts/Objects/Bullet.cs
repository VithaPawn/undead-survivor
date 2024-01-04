using UnityEngine;

public class Bullet : MonoBehaviour {

    private readonly float bulletDamage = 50f;

    private void OnCollisionEnter2D()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void MovingForward(Weapon weapon, float shootingForce)
    {
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.AddForce(weapon.GetWeaponDirectionNormalized() * shootingForce, ForceMode2D.Impulse);
    }

    public float GetBulletDamage() { return bulletDamage; }
}
