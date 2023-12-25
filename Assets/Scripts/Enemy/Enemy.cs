using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private EnemyVisual enemyVisual;

    private float bodyRemainTimer;
    private float currentHealth;
    private float currentRunSpeed;
    public bool isDie { set; get; }

    public event EventHandler OnHitted;
    public event EventHandler OnDie;

    private void Awake()
    {
        isDie = false;
        currentHealth = enemySO.health;
        currentRunSpeed = enemySO.speed;
        bodyRemainTimer = enemySO.bodyRemainDelay;
    }

    private void FixedUpdate()
    {
        if (!isDie)
        {
            // Control enemy movement
            Vector2 runDirection = PlayerMovement.Instance.transform.position - transform.position;
            Vector2 normalizedRunDirection = runDirection.normalized;
            transform.position += new Vector3(normalizedRunDirection.x, normalizedRunDirection.y, 0) * currentRunSpeed * Time.fixedDeltaTime;

            // Control enemy look at direction
            LookAt(runDirection);
        }
    }

    private void LookAt(Vector2 lookAtDirection)
    {
        if (lookAtDirection.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollideWithBullet(collision);
    }

    private void OnCollideWithBullet(Collider2D collision)
    {
        if (!isDie && collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.DestroySelf();
            StartCoroutine(LostHealthCoroutine(bullet.GetBulletDamage()));
        }
    }

    private IEnumerator LostHealthCoroutine(float damage)
    {
        //Set enemy status
        currentHealth -= damage;
        currentRunSpeed = 0f;
        //fire event to enemy visual when be hitted
        OnHitted?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(0.1f);
        //Check dead status
        if (currentHealth <= 0f)
        {
            isDie = true;
            OnDie?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DestroyWhenDie());
        }
        else
        {
            currentRunSpeed = enemySO.speed;
        }
    }

    private IEnumerator DestroyWhenDie()
    {
        yield return new WaitForSeconds(bodyRemainTimer);
        Destroy(gameObject);
    }

    public EnemyVisual GetEnemyVisual()
    {
        return enemyVisual;
    }

    public float GetEnemyDamageToPlayer()
    {
        return enemySO.damageToPlayer;
    }
}
