using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCanvas : BaseCanvas {
    #region Constants
    #endregion Constants

    #region Variables
    [Header("Children UIs")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI killAmountText;

    [Header("Experience Point")]
    [SerializeField] private FloatVariableSO expTotal;
    [SerializeField] private FloatVariableSO expRequired;
    [SerializeField] private FloatVariableSO expFloor;

    [Header("Kill Amount")]
    [SerializeField] private FloatVariableSO killAmount;

    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;

    #endregion Variables

    #region Methods
    private void Awake()
    {
        expBar.value = 0f;
        killAmountText.text = "0";
    }

    private void OnEnable()
    {
        expTotal.OnChanged += ExperienceTotal_Changed;
        gameStateManagerSO.OnChanged += GameStateManagerSO_OnChanged;
        killAmount.OnChanged += KillAmount_OnChanged;
    }

    private void OnDisable()
    {
        expTotal.OnChanged -= ExperienceTotal_Changed;
        gameStateManagerSO.OnChanged -= GameStateManagerSO_OnChanged;
        killAmount.OnChanged -= KillAmount_OnChanged;
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            CountTime();
        }
    }

    #region Timer
    private void CountTime()
    {
        float playingTimer = GameManager.Instance.GetPlayingTimeCounter();
        int integerPlayingTimer = (int)Math.Floor(playingTimer);
        UpdateTimerText(FormatTime(integerPlayingTimer));
    }

    static string FormatTime(int totalSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
    #endregion Timer

    #region Point
    private void ExperienceTotal_Changed()
    {
        UpdateExpBar();
    }

    private void KillAmount_OnChanged()
    {
        UpdateKillAmountText("");
    }
    #endregion Point

    #region UI
    private void GameStateManagerSO_OnChanged()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            UpdateExpBar();
            UpdateKillAmountText("0");
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void UpdateTimerText(string waveTitle)
    {
        timerText.text = waveTitle;
    }

    private void UpdateExpBar()
    {
        float relativeExpRequired = expRequired.GetValue() - expFloor.GetValue();
        float relativeExpTotal = expTotal.GetValue() - expFloor.GetValue();
        expBar.value = GetPercentageBetweenTwoNumbers(relativeExpRequired, relativeExpTotal);
    }

    private void UpdateKillAmountText(string defaultValue)
    {
        killAmountText.text = defaultValue != "" ? defaultValue : killAmount.GetValue().ToString();
    }
    #endregion UI

    private float GetPercentageBetweenTwoNumbers(float divisor, float dividened)
    {
        return Mathf.Clamp01(dividened / divisor);
    }
    #endregion Methods

}
