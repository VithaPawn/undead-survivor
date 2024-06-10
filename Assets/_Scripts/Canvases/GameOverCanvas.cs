using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : BaseCanvas {
    #region Variables
    [Header("Children UIs")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [Header("Game State")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    [SerializeField] private GameStateSO gameMenuStateSO;
    [SerializeField] private GameStateSO gamePlayingStateSO;
    [Header("Statistics")]
    [SerializeField] private FloatVariableSO defeatedEnemiesAmount;
    [SerializeField] private FloatVariableSO expPoints;
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

    protected override void Show()
    {
        scoreText.text = defeatedEnemiesAmount.GetValue().ToString();
        bestText.text = expPoints.GetValue().ToString();
        base.Show();
    }
    #endregion Methods
}
