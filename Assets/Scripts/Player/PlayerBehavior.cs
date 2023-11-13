using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
    public static PlayerBehavior Instance { get; private set; }

    // detech enemy collision timer
    private readonly float maxDamagedDetectTimer = 1f;
    private float damagedDetectTimer;

    public event EventHandler<OnBeHittedEventArgs> OnBeHitted;
    public class OnBeHittedEventArgs : EventArgs {
        public Enemy collidedEnemy;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one PlayerBehavior at the same time!");
        }
        else
        {
            Instance = this;
        }
        damagedDetectTimer = 0;
    }

    private void Update()
    {
        Enemy collidedEnemy = GetCollidedEnemy();
        if (collidedEnemy != null)
        {
            ControlDamageDetectTimer();
            if (damagedDetectTimer == 0)
            {
                OnBeHitted?.Invoke(this, new OnBeHittedEventArgs
                {
                    collidedEnemy = collidedEnemy
                });
            }
        }
    }

    private Enemy GetCollidedEnemy()
    {
        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> colliderList = new List<Collider2D>();
        int overlapNumber = Physics2D.OverlapCollider(coll, filter, colliderList);
        if (overlapNumber > 0)
        {
            foreach (Collider2D collider2D in colliderList)
            {
                if (collider2D.gameObject.TryGetComponent(out Enemy enemy))
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    private void ControlDamageDetectTimer()
    {
        damagedDetectTimer += Time.deltaTime;
        if (damagedDetectTimer >= maxDamagedDetectTimer)
        {
            damagedDetectTimer = 0;
        }
    }
}
