using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : BaseCanvas {
    #region Constants
    private const string BEST_SCORE = "US_BestScore";
    #endregion Constants

    #region Variables
    [Header("Children UIs")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private Image newIcon;
    [Header("Game State")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    [SerializeField] private GameStateSO gameMenuStateSO;
    [SerializeField] private GameStateSO gamePlayingStateSO;
    [Header("Statistics")]
    [SerializeField] private FloatVariableSO expPoints;
    private float bestScore;
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
        //Get best score from local memory
        bestScore = PlayerPrefs.GetFloat(BEST_SCORE, 0);
        float currentScore = expPoints.GetValue();

        //Update UI
        scoreText.text = expPoints.GetValue().ToString();
        if (bestScore < currentScore)
        {
            bestText.text = currentScore.ToString();
            newIcon.gameObject.SetActive(true);
            PlayerPrefs.SetFloat(BEST_SCORE, currentScore);
        }
        else
        {
            newIcon.gameObject.SetActive(false);
            bestText.text = bestScore.ToString();
        }
        base.Show();
    }
    #endregion Methods
}
