using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    [SerializeField] private List<EnemySO> enemySOList;

    private readonly float waveDuration = 20f;
    private int sortingOrderOffset = 0;
    private float sortingOrderTimer = 60f;
    private float enemySpawnTimerMax;
    private float enemySpawnTimer;

    private void Awake()
    {
        enemySpawnTimerMax = waveDuration / 5;
        enemySpawnTimer = enemySpawnTimerMax;
    }

    private void Update()
    {
        ControlEnemySpawn();
        ControlEnemySortingLayerTimer();
    }

    private void ControlEnemySpawn()
    {
        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0)
        {
            SpawnEnemy();
            enemySpawnTimer = enemySpawnTimerMax;
        }
    }

    private void ControlEnemySortingLayerTimer()
    {
        sortingOrderTimer -= Time.deltaTime;
        if (sortingOrderTimer <= 0)
        {
            sortingOrderOffset = 0;
        }
    }

    private void SpawnEnemy()
    {
        EnemySO enemySO = enemySOList[Random.Range(0, enemySOList.Count)];
        Enemy enemy = Instantiate(enemySO.enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.GetEnemyVisual().SetPlayerVisualSortingOrder(sortingOrderOffset++);
    }
}
