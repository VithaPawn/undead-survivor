using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BooleanVariableSO", menuName = "ScriptableObjects/Variables/BooleanVariableSO")]
public class BooleanVariableSO : ScriptableObject {
    #region Variables
    [Header("This variable can be changed or not")]
    [SerializeField] private bool canChanged;

    [Header("Variable values")]
    [SerializeField] private bool _initialValue = true;
    [SerializeField] private bool _currentValue;

    public event Action OnChanged;
    #endregion Variables

    #region Methods
    private void OnEnable()
    {
        _currentValue = _initialValue;
    }

    public bool GetValue()
    {
        return _currentValue;
    }

    public void SetValue(bool value)
    {
        if (!canChanged) { return; }
        bool oldValue = _currentValue;
        _currentValue = value;
        if (oldValue != _currentValue)
        {
            OnChanged?.Invoke();
        }
    }
    #endregion Methods
}

