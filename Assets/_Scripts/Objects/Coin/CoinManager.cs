using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class CoinManager : MonoBehaviour {
    #region Singleton
    static public CoinManager Instance { get; private set; }
    #endregion Singleton

    #region Variables
    [SerializeField] private Transform coinsParent;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
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
        Coin.SpawnCoin(expSO, spawnPosition, coinsParent);
    }

    private void ClearCoins()
    {
        foreach (Transform coinTransform in coinsParent)
        {
            Destroy(coinTransform.gameObject);
        }
    }
    #endregion Methods
}
