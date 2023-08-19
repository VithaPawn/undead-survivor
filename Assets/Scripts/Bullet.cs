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

    public float GetBulletDamage() { return bulletDamage; }
}
