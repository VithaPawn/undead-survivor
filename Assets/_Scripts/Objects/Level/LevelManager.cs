using UnityEngine;

public class LevelManager : MonoBehaviour {
    #region Constants
    private const float FIRST_VARIABLE_OF_LEVEL_FORMULA = 0.1f;
    private const float SECOND_VARIABLE_OF_LEVEL_FORMULA = 2f;
    #endregion Constants

    #region Variables
    [SerializeField] private FloatVariableSO expTotal;
    [SerializeField] private FloatVariableSO expRequired;
    [SerializeField] private FloatVariableSO expFloor;
    [SerializeField] private FloatVariableSO currentLevel;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    #endregion Variables

    #region Methods
    private void Start()
    {
        SetExpValues();
    }
    #region Listener
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
    #endregion Listener

    private void ExperienceTotal_Changed()
    {
        if (expTotal.GetValue() >= expRequired.GetValue())
        {
            currentLevel.Increase(1);
            SetExpValues();
        }
    }

    private void GameStateManagerSO_OnChanged()
    {
        if (gameStateManagerSO.IsGameOver())
        {
            currentLevel.ResetValue();
            expTotal.ResetValue();
            expFloor.ResetValue();
            expRequired.ResetValue();
            SetExpValues();
        }
    }

    private float CalculateExpRequired(float level)
    {
        return Mathf.Pow(level / FIRST_VARIABLE_OF_LEVEL_FORMULA, SECOND_VARIABLE_OF_LEVEL_FORMULA);
    }

    private void SetExpValues()
    {
        expFloor.SetValue(expRequired.GetValue(), false);
        float nextRequiredExperience = CalculateExpRequired(currentLevel.GetValue());
        expRequired.SetValue(nextRequiredExperience);
    }
    #endregion Methods
}
