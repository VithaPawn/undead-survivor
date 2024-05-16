using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : BaseCanvas {
    #region Constants
    private const float MUSIC_MULTIPLIER = 25;
    #endregion Constants

    #region Variables
    [Header("Children UIs")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Slider soundBar;
    [SerializeField] private Slider musicBar;
    [Header("Game State")]
    [SerializeField] private GameStateSO gameMenuStateSO;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    [Header("Audio Parameters")]
    [SerializeField] private FloatVariableSO soundVolume;
    [SerializeField] private FloatVariableSO musicVolume;
    [Header("Other Canvases")]
    [SerializeField] private MenuCanvas menuCanvas;

    public event Action OnCloseOption;
    #endregion Variables

    #region Methods
    private void OnEnable()
    {
        menuCanvas.OnOpenOption += MenuCanvas_OnOpenOption;
    }

    private void OnDisable()
    {
        menuCanvas.OnOpenOption -= MenuCanvas_OnOpenOption;
    }

    private void Start()
    {
        musicBar.onValueChanged.AddListener(OnSliderValueChanged);
        closeButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            SaveAudioVolume();
            Hide();
        });
        Hide();
    }

    private void MenuCanvas_OnOpenOption()
    {
        Show();
    }

    private void OnSliderValueChanged(float value)
    {
        musicVolume.SetValue(Mathf.Clamp(value / MUSIC_MULTIPLIER, 0, 0.04f));
    }

    private void SaveAudioVolume()
    {
        OnCloseOption?.Invoke();
        soundVolume.SetValue(Mathf.Clamp01(soundBar.value));
    }

    protected override void Show()
    {
        soundBar.value = soundVolume.GetValue();
        //Set music bar value and do not trigger change value event
        musicBar.onValueChanged.RemoveListener(OnSliderValueChanged);
        musicBar.value = musicVolume.GetValue() * MUSIC_MULTIPLIER;
        musicBar.onValueChanged.AddListener(OnSliderValueChanged);
        base.Show();
    }
    #endregion Methods
}
