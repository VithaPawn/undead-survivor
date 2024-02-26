using DatabaseSystem.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    #region Constants
    private const int WAVE_AMOUNT_EACH_DIFFICULT_LEVEL = 3;
    private const float WAVE_VALUE_ADDITION_EACH_DIFFICULT_LEVEL = 8f;
    private const float WAVE_TIME_ADDITION_EACH_DEFFICULT_LEVEL = 2f;
    #endregion Constants

    #region Variables
    [SerializeField] private List<Enemy> enemyTotalList;
    private List<Enemy> enemyEnableToSpawnList;
    private List<Enemy> nextWaveEnemies;
    private SpawnArea spawnArea;
    private int waveCounter;
    private int waveDifficulty;
    private float waveValue;
    private float waveDurationTimerMax;
    private float waveDurationTimer;

    #region Events
    public event EventHandler<OnWaveChangesEventArgs> OnWaveChanges;
    public class OnWaveChangesEventArgs : EventArgs { public int waveCount; }
    #endregion Events
    #endregion Variables

    #region Methods
    private void Awake()
    {
        spawnArea = GetComponent<SpawnArea>();
        enemyEnableToSpawnList = new List<Enemy>();
        nextWaveEnemies = new List<Enemy>();
        waveCounter = 1;
        waveDifficulty = 1;
        waveValue = 12f;
        waveDurationTimerMax = 10f;
        waveDurationTimer = 2f;
    }

    private void Start()
    {
        SetEnableToSpawnEnemies();
        GenerateNextWaveEnemies();
    }

    private void SetEnableToSpawnEnemies()
    {
        if (waveDifficulty <= enemyTotalList.Count)
        {
            enemyEnableToSpawnList = enemyTotalList.GetRange(0, waveDifficulty);
        }
        else
        {
            enemyEnableToSpawnList = enemyTotalList;
        }
    }

    private void GenerateNextWaveEnemies()
    {
        List<Enemy> generatedEnemies = new List<Enemy> { };
        List<Enemy> enableForSpawningEnemies = enemyEnableToSpawnList;
        float nextWaveRemainingValue = waveValue;
        while (nextWaveRemainingValue > 0)
        {
            int randomEnemyId = UnityEngine.Random.Range(0, enableForSpawningEnemies.Count);
            float randomWaveValue = enableForSpawningEnemies[randomEnemyId].value;

            if (nextWaveRemainingValue - randomWaveValue >= 0)
            {
                generatedEnemies.Add(enemyEnableToSpawnList[randomEnemyId]);
                nextWaveRemainingValue -= randomWaveValue;
            }
            else
            {
                enableForSpawningEnemies.RemoveAt(randomEnemyId);
                break;
            }
        }
        nextWaveEnemies.Clear();
        nextWaveEnemies = generatedEnemies;
    }


    private void Update()
    {
        HandleEnemySpawn();
    }

    private void HandleEnemySpawn()
    {
        waveDurationTimer -= Time.deltaTime;
        if (waveDurationTimer <= 0)
        {
            waveDurationTimer = waveDurationTimerMax;

            SpawnEnemy(nextWaveEnemies);
            HandleWaveDifficulty();
            GenerateNextWaveEnemies();

            OnWaveChanges?.Invoke(this, new OnWaveChangesEventArgs
            {
                waveCount = waveCounter
            });
        }
    }

    private void SpawnEnemy(List<Enemy> enemySOs)
    {
        Vector3 playerPosition = PlayerMovement.Instance.GetPlayerPosition();
        foreach (var enemy in enemySOs)
        {
            Vector3 spawnPosition = spawnArea.GetRelativePawnPositionByLandmark(playerPosition);
            PlayingObjects.Enemy.SpawnEnemy(enemy, spawnPosition);
        }
    }

    private void HandleWaveDifficulty()
    {
        if (waveCounter % WAVE_AMOUNT_EACH_DIFFICULT_LEVEL == 0)
        {
            waveDifficulty++;
            SetWaveAttributes();
        }
        waveCounter++;
    }

    private void SetWaveAttributes()
    {
        waveDurationTimerMax += WAVE_TIME_ADDITION_EACH_DEFFICULT_LEVEL;
        waveValue += WAVE_VALUE_ADDITION_EACH_DIFFICULT_LEVEL;
        SetEnableToSpawnEnemies();
    }
    #endregion Methods
}
