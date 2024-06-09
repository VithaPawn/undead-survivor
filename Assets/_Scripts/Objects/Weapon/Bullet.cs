using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IDamageObject {
    [Header("Explode Effect")]
    [SerializeField] private ParticleSystemChannelSO explodeEffect;
    [Header("Parent Weapon Pool")]
    private IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool { set { objectPool = value; } }

    public float Damage { get; set; } = 50f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D()
    {
        Explode();
    }

    public void Explode()
    {
        if (!gameObject.activeSelf) return;
        explodeEffect.RaiseEvent(transform.position);
        objectPool.Release(this);
    }

    public void MovingForward(Vector2 weaponDirection, float shootingForce)
    {
        rb.AddForce(weaponDirection * shootingForce, ForceMode2D.Impulse);
    }
}
