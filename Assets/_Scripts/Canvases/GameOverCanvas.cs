using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : BaseCanvas {
    #region Variables
    [Header("Children UIs")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;
    [Header("Game State")]
    [SerializeField] private GameStateSO gameMenuStateSO;
    [SerializeField] private GameStateSO gamePlayingStateSO;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    #endregion Variables

    #region Methods
    private void Start()
    {
        Hide();
        replayButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            gameStateManagerSO.ChangeState(gamePlayingStateSO);
        });
        homeButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            gameStateManagerSO.ChangeState(gameMenuStateSO);
        });
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
        if (gameStateManagerSO.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    #endregion Methods
}
