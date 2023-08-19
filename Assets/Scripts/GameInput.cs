using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    private InputActionSystem inputActionSystem;
    public event EventHandler OnShoot;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one GameInput instance.");
        }
        else
        {
            Instance = this;
        }
        inputActionSystem = new InputActionSystem();
        inputActionSystem.Player.Enable();
        inputActionSystem.Player.Shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShoot?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = inputActionSystem.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
