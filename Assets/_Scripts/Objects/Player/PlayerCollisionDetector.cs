using PlayingObjects;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour {

    private const float DAMAGE_DETECT_TIME = 1f;

    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;
    [Header("Sound on hurt")]
    [SerializeField] private SoundChannelSO hurtSound;
    [Header("Collision")]
    private float damagedDetectTimer;
    private Collider2D coll;

    private void Awake()
    {
        damagedDetectTimer = 0;
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Enemy collidedEnemy = GetCollidedEnemy();
        if (collidedEnemy != null && !collidedEnemy.isDie)
        {
            if (damagedDetectTimer <= 0)
            {
                hurtSound.RaiseEvent(transform.position);
                playerHealth.Decrease(collidedEnemy.GetEnemyDamageToPlayer());
            }
            ControlDamageDetectTimer();
        }
        else
        {
            damagedDetectTimer = 0;
        }
    }

    private Enemy GetCollidedEnemy()
    {
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
        if (damagedDetectTimer >= DAMAGE_DETECT_TIME)
        {
            damagedDetectTimer = 0;
        }
    }
}
