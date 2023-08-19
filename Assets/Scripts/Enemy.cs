using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private readonly float maxHealth = 100f;
    private float runSpeed = 0.5f;

    private Vector2 runDirection;
    private float currentHealth;
    private bool isDie = false;
    private float bodyRemainsAfterDieTimer = 1f;
    public event EventHandler OnHitted;
    public event EventHandler OnDie;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDie)
        {
            bodyRemainsAfterDieTimer -= Time.deltaTime;
            if (bodyRemainsAfterDieTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        runDirection = Player.Instance.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        // Control enemy movement
        Vector3 enemyPosition = transform.position;
        enemyPosition += new Vector3(runDirection.x, runDirection.y, 0) * runSpeed * Time.fixedDeltaTime;
        transform.position = enemyPosition;

        // Control enemy look at direction
        if (runDirection.x >= 0)
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
        if (!isDie && collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            bullet.DestroySelf();
            currentHealth -= bullet.GetBulletDamage();
            runSpeed = 0f;
            OnHitted?.Invoke(this, EventArgs.Empty);
            if (currentHealth <= 0f)
            {
                isDie = true;
                OnDie?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                runSpeed = 1f;
            }
        }
    }

}
