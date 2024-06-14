using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : BaseCanvas {
    #region Constants
    private const string BEST_SCORE = "US_BestScore";
    #endregion Constants

    #region Variables
    [Header("Children UI Components")]
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    [SerializeField] private Image newIcon;
    [SerializeField] private Transform upgradeIconList;
    [SerializeField] private Transform upgradeIconTemplate;

    [Header("Game States")]
    [SerializeField] private GameStateSO gameMenuStateSO;
    [SerializeField] private GameStateSO gamePlayingStateSO;

    [Header("Managers")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    //[SerializeField] private UpgradeManager

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

        //Update score UI
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

        //Update upgrades UI
        Dictionary<int, UpgradeData> playingUpgradeSystems = UpgradeManager.Instance.GetPlayingUpgradeSystemDict();
        for (int i = 0; i < playingUpgradeSystems.Count; i++)
        {
            Transform upgradeIconTransform = Instantiate(upgradeIconTemplate, upgradeIconList);
            upgradeIconTransform.gameObject.SetActive(true);
            if (upgradeIconTransform.TryGetComponent(out UpgradeIconSingle upgradeIcon))
            {
                upgradeIcon.UpdateGUI(playingUpgradeSystems.ElementAt(i).Value);
            }
        }

        base.Show();
    }

    protected override void Hide()
    {
        foreach (Transform upgradeIcon in upgradeIconList)
        {
            if (upgradeIcon != upgradeIconTemplate)
            {
                Destroy(upgradeIcon.gameObject);
            }
            else if (upgradeIcon == upgradeIconTemplate)
            {
                upgradeIcon.gameObject.SetActive(false);
            }
        }

        base.Hide();
    }
    #endregion Methods
}
