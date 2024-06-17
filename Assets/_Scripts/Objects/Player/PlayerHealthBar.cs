using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [Header("Health Value")]
    [SerializeField] private FloatVariableSO currentHealthValue;
    [SerializeField] private FloatVariableSO maxHealthValue;

    [Header("Health Bar")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private float additionalBarLength = 10;
    private RectTransform rectTransform;

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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + additionalBarLength, rectTransform.sizeDelta.y);
    }

    private bool IsPlayerFullHealth()
    {
        return currentHealthValue.GetValue() == maxHealthValue.GetValue();
    }

    private bool IsPlayerDead()
    {
        return currentHealthValue.GetValue() <= 0;
    }
}
