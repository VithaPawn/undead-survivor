using DatabaseSystem.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;

namespace PlayingObjects {
    public class Enemy : MonoBehaviour {
        #region Variables
        public bool isDie { set; get; }
        [SerializeField] private FloatVariableSO defeatedEnemiesAmount;
        [SerializeField] private DatabaseSystem.ScriptableObjects.Enemy enemySO;
        private EnemyVisual enemyVisual;
        private float currentHealth;
        private float currentRunSpeed;
        private ExpPoint expPoint;

        #region Events
        public event EventHandler OnHitted;
        #endregion Events
        #endregion Variables

        #region Methods
        #region Spawn
        public static Enemy SpawnEnemy(DatabaseSystem.ScriptableObjects.Enemy enemySO, Vector3 spawnPosition, Transform parentObejct)
        {
            Enemy enemy = Instantiate(enemySO.prefab, spawnPosition, Quaternion.identity);
            enemy.Initialize();
            if (parentObejct) { enemy.transform.SetParent(parentObejct); }
            return enemy;
        }

        private void Initialize()
        {
            enemyVisual = GetComponentInChildren<EnemyVisual>();
            isDie = false;
            currentHealth = enemySO.health;
            currentRunSpeed = enemySO.speed;
            expPoint = enemySO.GetRandomExpPoint();
        }
        #endregion Spawn

        #region Movement
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
        #endregion Movement

        #region Collision
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollideWithDamageObject(collision);
        }

        private void OnCollideWithDamageObject(Collider2D collision)
        {
            if (!isDie && collision.gameObject.TryGetComponent(out IDamageObject damageObj))
            {
                StartCoroutine(LostHealthCoroutine(damageObj.Damage));
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
                defeatedEnemiesAmount.Increase(1);
                CoinManager.Instance.GenerateCoin(expPoint, gameObject.transform.position);
                Destroy(gameObject);
            }
            else
            {
                currentRunSpeed = enemySO.speed;
            }
        }
        #endregion Collision

        #region VariableGetters
        public EnemyVisual GetEnemyVisual()
        {
            return enemyVisual;
        }

        public float GetEnemyDamageToPlayer()
        {
            return enemySO.damageToPlayer;
        }
        #endregion VariableGetters
        #endregion Methods
    }
}
