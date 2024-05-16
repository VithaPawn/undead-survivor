using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateManagerSO", menuName = "ScriptableObjects/GameState/GameStateManagerSO")]
public class GameStateManagerSO : ScriptableObject {
    #region Variables
    [Header("Initial State")]
    [SerializeField] private GameStateSO _initialValue;
    [Header("Current State")]
    [SerializeField] private GameStateSO _currentValue;
    [Header("Available State")]
    [SerializeField] private GameStateSO _gameMenuState;
    [SerializeField] private GameStateSO _gamePlayingState;
    [SerializeField] private GameStateSO _gameOverState;
    public event Action OnChanged;
    #endregion Variables

    #region Methods
    private void OnEnable()
    {
        ResetValue();
    }

    public void ResetValue()
    {
        _currentValue = _initialValue;
    }

    public void ChangeState(GameStateSO value, bool isTriggerEvent = true)
    {
        _currentValue = value;
        if (isTriggerEvent)
        {
            OnChanged?.Invoke();
        }
    }

    public GameStateSO GetState()
    {
        return _currentValue;
    }

    public bool IsGameMenu()
    {
        return _currentValue == _gameMenuState;
    }
    public bool IsGamePlaying()
    {
        return _currentValue == _gamePlayingState;
    }
    public bool IsGameOver()
    {
        return _currentValue == _gameOverState;
    }
    #endregion Methods
}