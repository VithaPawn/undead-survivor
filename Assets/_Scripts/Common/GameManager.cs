using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Variables
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    [SerializeField] private GameStateSO gameOverStateSO;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    [Header("Information need to reset")]
    [SerializeField] private FloatVariableSO playerHealth;
    [SerializeField] private UpgradePool upgradePool;
    [Header("Playing Time Counter")]
    private float playingTimeCounter = 0f;
    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerObject;
    [Header("Game Over Sound")]
    [SerializeField] private SoundChannelSO gameoverSound;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Game Manager instance!");
        }
        else
        {
            Instance = this;
        }
        Application.targetFrameRate = 120;
    }

    private void OnEnable()
    {
        playerHealth.OnChanged += PlayerHealth_OnChanged;
        gameStateManagerSO.OnChanged += GameStateManagerSO_OnChanged;
    }

    private void OnDisable()
    {
        playerHealth.OnChanged -= PlayerHealth_OnChanged;
        gameStateManagerSO.OnChanged -= GameStateManagerSO_OnChanged;
    }

    private void Start()
    {
        //Find and set active for player
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            playerObject = player;
        }
        playerObject.SetActive(false);
    }

    private void Update()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            playingTimeCounter += Time.deltaTime;
        }
    }

    private void PlayerHealth_OnChanged()
    {
        if (gameStateManagerSO.IsGamePlaying() && playerHealth.GetValue() <= 0)
        {
            StartCoroutine(changeGameOverCoroutine());
        }
    }

    private void GameStateManagerSO_OnChanged()
    {
        StopAllCoroutines();
        if (gameStateManagerSO.IsGamePlaying())
        {
            playingTimeCounter = 0f;
            playerHealth.ResetValue();
            upgradePool.ResetAvailablePool();
            playerObject.transform.position = Vector3.zero;
            playerObject.SetActive(true);
        }
        else
        {
            playerObject.SetActive(false);
        }
        if (gameStateManagerSO.IsGameMenu())
        {
            playerObject.transform.position = Vector3.zero;
        }
    }

    public float GetPlayingTimeCounter() { return playingTimeCounter; }

    private IEnumerator changeGameOverCoroutine()
    {
        gameoverSound.RaiseEvent(playerObject.transform.position);
        yield return new WaitForSeconds(0.5f);
        gameStateManagerSO.ChangeState(gameOverStateSO);
    }

    #endregion Methods
}
