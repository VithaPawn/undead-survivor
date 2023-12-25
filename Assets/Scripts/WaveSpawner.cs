using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    [SerializeField] private List<EnemySO> enemySOList;

    private readonly float waveDuration = 20f;
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

    private void SpawnEnemy()
    {
        EnemySO enemySO = enemySOList[Random.Range(0, enemySOList.Count)];
        Instantiate(enemySO.enemyPrefab, new Vector3(0, 0, 0.001f), Quaternion.identity);
    }
}
