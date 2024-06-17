using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariableSO currentHealth;
    [SerializeField] private FloatVariableSO maxHealth;

    private void OnEnable()
    {
        currentHealth.OnChanged += CurrentHealth_OnChanged;
    }

    private void OnDisable()
    {
        currentHealth.OnChanged -= CurrentHealth_OnChanged;
    }

    private void CurrentHealth_OnChanged()
    {
        if (currentHealth.GetValue() > maxHealth.GetValue()) { 
            currentHealth.SetValue(maxHealth.GetValue(), false);
        }
    }
}
