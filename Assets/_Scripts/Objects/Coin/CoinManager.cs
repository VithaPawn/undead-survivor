using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;

public class CoinManager : MonoBehaviour {
    #region Singleton
    static public CoinManager Instance { get; private set; }
    #endregion Singleton

    #region Variables
    [SerializeField] private Transform coinStoreTransform;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;

    [SerializeField] private Coin coinPrefab;
    protected IObjectPool<Coin> coinPool;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one EffectManager at the same time!");
        }
        else
        {
            Instance = this;
        }
        coinPool = new ObjectPool<Coin>(CreateCoin, OnGetFromPool, OnReleaseToPool, OnDestroyPooledCoin, defaultCapacity: 100);
    }

    private void OnEnable()
    {
        gameStateManagerSO.OnChanged += GameStateManagerSO_OnChanged;
    }

    private void OnDisable()
    {
        gameStateManagerSO.OnChanged -= GameStateManagerSO_OnChanged;
    }

    private void GameStateManagerSO_OnChanged()
    {
        if (gameStateManagerSO.IsGameMenu() || gameStateManagerSO.IsGamePlaying())
        {
            ClearCoins();
        }
    }

    public void GenerateCoin(ExpPoint expSO, Vector3 spawnPosition)
    {
        Coin coinInstance = coinPool.Get();
        coinInstance.transform.SetParent(coinStoreTransform);
        coinInstance.transform.position = spawnPosition;
        coinInstance.SetCoinValue(expSO);
    }

    private void ClearCoins()
    {
        foreach (Transform coinTransform in coinStoreTransform)
        {
            if (coinTransform.TryGetComponent(out Coin coin) && coin.gameObject.activeSelf)
            {
                coinPool.Release(coin);
            }
        }
    }
    private Coin CreateCoin()
    {
        Coin coinInstance = Instantiate(coinPrefab);
        coinInstance.CoinPool = coinPool;
        return coinInstance;
    }

    private void OnGetFromPool(Coin pooledCoin)
    {
        pooledCoin.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Coin pooledCoin)
    {
        pooledCoin.gameObject.SetActive(false);
    }

    private void OnDestroyPooledCoin(Coin pooledCoin)
    {
        Destroy(pooledCoin.gameObject);
    }
    #endregion Methods
}
