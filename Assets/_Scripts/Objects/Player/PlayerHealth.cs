using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManager;

    [Header("Health Statistics")]
    [SerializeField] private FloatVariableSO currentHealth;
    [SerializeField] private FloatVariableSO maxHealth;

    private void OnEnable()
    {
        currentHealth.OnChanged += CurrentHealth_OnChanged;
        gameStateManager.OnChanged += GameStateManager_OnChanged;
    }


    private void OnDisable()
    {
        currentHealth.OnChanged -= CurrentHealth_OnChanged;
        gameStateManager.OnChanged -= GameStateManager_OnChanged;
    }

    private void CurrentHealth_OnChanged()
    {
        if (currentHealth.GetValue() > maxHealth.GetValue())
        {
            currentHealth.SetValue(maxHealth.GetValue(), false);
        }
    }
    private void GameStateManager_OnChanged()
    {
        if (gameStateManager.IsGamePlaying())
        {
            currentHealth.ResetValue();
            maxHealth.ResetValue();
        }
    }
}
