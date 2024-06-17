using DatabaseSystem.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IDamageObject {
    #region Constants
    private const string ENEMY_TAG = "Enemy";
    private const string OBSTACLE_TAG = "Obstacle";
    #endregion Constants


    [Tooltip("Does bullet expole when colliding with enemies")]
    [SerializeField] private bool isCollideWithEnemies;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollideWithEnemies && other.gameObject.CompareTag(ENEMY_TAG))
        {
            Explode();
        }
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
    public IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        if (gameObject.activeSelf)
        {
            objectPool.Release(this);
        }
    }
}
