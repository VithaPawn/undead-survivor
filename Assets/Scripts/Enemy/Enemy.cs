using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private EnemySO enemySO;

    private float bodyRemainTimer;
    private float currentHealth;
    private float currentRunSpeed;
    private bool isDie = false;

    public event EventHandler OnHitted;
    public event EventHandler OnDie;

    private void Awake()
    {
        currentHealth = enemySO.health;
        currentRunSpeed = enemySO.speed;
        bodyRemainTimer = enemySO.bodyRemainDelay;
    }

    private void Update()
    {
        if (isDie)
        {
            bodyRemainTimer -= Time.deltaTime;
            if (bodyRemainTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollideWithBullet(collision);
    }

    private void OnCollideWithBullet(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.DestroySelf();

            //Set status for enemy
            currentHealth -= bullet.GetBulletDamage();
            currentRunSpeed = 0f;

            //fire event to enemy visual when be hitted
            OnHitted?.Invoke(this, EventArgs.Empty);

            //Check dead status
            CheckDeadStatus();
        }
    }


    private void CheckDeadStatus()
    {
        if (currentHealth <= 0f)
        {
            isDie = true;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            currentRunSpeed = enemySO.speed;
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

    public EnemyVisual GetEnemyVisual()
    {
        Transform enemyVisual = transform.Find("EnemyVisual");
        EnemyVisual enemyVisualScript = enemyVisual.GetComponent<EnemyVisual>();
        return (enemyVisualScript);
    }

    public float GetEnemyDamageToPlayer()
    {
        return enemySO.damageToPlayer;
    }
}
