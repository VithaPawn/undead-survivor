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

    [Header("Experience Point")]
    [SerializeField] private FloatVariableSO expTotal;
    [SerializeField] private FloatVariableSO expRequired;
    [SerializeField] private FloatVariableSO expFloor;

    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;

    #endregion Variables

    #region Methods
    private void Awake()
    {
        expBar.value = 0f;
    }

    private void OnEnable()
    {
        expTotal.OnChanged += ExperienceTotal_Changed;
        gameStateManagerSO.OnChanged += GameStateManagerSO_OnChanged;
    }

    private void OnDisable()
    {
        expTotal.OnChanged -= ExperienceTotal_Changed;
        gameStateManagerSO.OnChanged -= GameStateManagerSO_OnChanged;
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
    #endregion Point

    #region UI
    private void GameStateManagerSO_OnChanged()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            UpdateExpBar();
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
    #endregion UI

    private float GetPercentageBetweenTwoNumbers(float divisor, float dividened)
    {
        return Mathf.Clamp01(dividened / divisor);
    }
    #endregion Methods

}
