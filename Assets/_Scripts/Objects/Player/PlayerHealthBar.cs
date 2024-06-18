using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [Header("Health Value")]
    [SerializeField] private FloatVariableSO currentHealthValue;
    [SerializeField] private FloatVariableSO maxHealthValue;

    [Header("Health Bar")]
    [SerializeField] private RectTransform healthBarParentTransform;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float additionalBarLength = 10;

    private void OnEnable()
    {
        currentHealthValue.OnChanged += CurrentHealthValue_OnChanged;
        maxHealthValue.OnChanged += MaxHealthValue_OnChanged;
    }

    private void OnDisable()
    {
        currentHealthValue.OnChanged -= CurrentHealthValue_OnChanged;
        maxHealthValue.OnChanged -= MaxHealthValue_OnChanged;
    }

    private void Start()
    {
        if (IsPlayerFullHealth() || IsPlayerDead())
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    private void CurrentHealthValue_OnChanged()
    {
        if (IsPlayerFullHealth() || IsPlayerDead())
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = Mathf.Clamp01(currentHealthValue.GetValue() / maxHealthValue.GetValue());
        }
    }
    private void MaxHealthValue_OnChanged()
    {
        healthBarParentTransform.sizeDelta = new Vector2(healthBarParentTransform.sizeDelta.x + additionalBarLength, healthBarParentTransform.sizeDelta.y);
        healthBar.value = Mathf.Clamp01(currentHealthValue.GetValue() / maxHealthValue.GetValue());
    }

    private bool IsPlayerFullHealth() => currentHealthValue.GetValue() == maxHealthValue.GetValue();

    private bool IsPlayerDead()
    {
        return currentHealthValue.GetValue() <= 0;
    }
}
