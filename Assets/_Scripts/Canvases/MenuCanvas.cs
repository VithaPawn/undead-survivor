using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : BaseCanvas {
    #region Variables
    [Header("Children UIs")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;
    [Header("Game State")]
    [SerializeField] private GameStateSO gamePlayingStateSO;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;

    public event Action OnOpenOption;
    #endregion Variables

    #region Methods
    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            gameStateManagerSO.ChangeState(gamePlayingStateSO);
        });
        optionButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            OnOpenOption?.Invoke();
        });
        quitButton.onClick.AddListener(() =>
        {
            PlayClickButtonSound(Vector3.zero);
            Application.Quit();
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
        if (gameStateManagerSO.IsGameMenu())
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
