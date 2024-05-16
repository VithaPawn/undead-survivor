using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariableSO", menuName = "ScriptableObjects/Variables/FloatVariableSO")]
public class FloatVariableSO : ScriptableObject {
    #region Variables
    [Header("Initial Value")]
    [SerializeField] private float _initialValue = 0;
    [Header("Playing Value")]
    [SerializeField] private float _currentValue = 0;
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

    public void Increase(float additionalValue, bool isTriggerEvent = true)
    {
        _currentValue += additionalValue;
        if (isTriggerEvent) { OnChanged?.Invoke(); }
    }

    public void Decrease(float minusValue, bool isTriggerEvent = true)
    {
        _currentValue -= minusValue;
        if (isTriggerEvent) { OnChanged?.Invoke(); }
    }

    public float GetValue()
    {
        return _currentValue;
    }

    public void SetValue(float value, bool isTriggerEvent = true)
    {
        _currentValue = value;
        if (isTriggerEvent)
        {
            OnChanged?.Invoke();
        }
    }

    public float GetInitialValue() { return _initialValue; }
    #endregion Methods

}

