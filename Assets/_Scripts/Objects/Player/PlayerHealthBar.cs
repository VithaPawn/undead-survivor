using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [Header("Health Value")]
    [SerializeField] private FloatVariableSO healthValue;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;

    private void OnEnable()
    {
        healthValue.OnChanged += HealthValue_OnChanged;
    }

    private void OnDisable()
    {
        healthValue.OnChanged -= HealthValue_OnChanged;
    }

    private void Start()
    {
        if (IsPlayerFullHealth() || IsPlayerDead())
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    private void HealthValue_OnChanged()
    {
        if (IsPlayerFullHealth() || IsPlayerDead())
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = Mathf.Clamp01(healthValue.GetValue() / healthValue.GetInitialValue());
        }
    }

    private bool IsPlayerFullHealth()
    {
        return healthValue.GetValue() == healthValue.GetInitialValue();
    }

    private bool IsPlayerDead()
    {
        return healthValue.GetValue() <= 0;
    }
}
