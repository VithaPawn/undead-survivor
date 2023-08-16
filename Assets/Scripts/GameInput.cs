using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }

    private InputActionSystem inputActionSystem;

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
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = inputActionSystem.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
