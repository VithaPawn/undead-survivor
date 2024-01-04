using System;
using TMPro;
using UnityEngine;

public class PlayingCanvas : MonoBehaviour {
    #region Constants
    #endregion Constants

    #region Variables
    [Header("Children UIs")]
    [SerializeField] private TextMeshProUGUI waveTitle;

    private float playingTimer = 0f;
    #endregion Variables

    #region Methods
    private void Update()
    {
        playingTimer += Time.deltaTime;
        int integerPlayingTimer = (int)Math.Floor(playingTimer);
        SetWaveTitle(FormatTime(integerPlayingTimer));
    }

    static string FormatTime(int totalSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    private void SetWaveTitle(string waveTitle)
    {
        this.waveTitle.text = waveTitle;
    }
    #endregion Methods

}
