using UnityEngine;

public class PlayerDisplayment : MonoBehaviour {
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManager;

    private void Start()
    {
        Hide();
    }

    private void OnEnable()
    {
        gameStateManager.OnChanged += GameStateManager_OnChanged;
    }

    private void OnDisable()
    {
        gameStateManager.OnChanged -= GameStateManager_OnChanged;
    }

    private void GameStateManager_OnChanged()
    {
        if (gameStateManager.IsGamePlaying())
        {
            transform.position = Vector3.zero;
            Show();
        }
        else
        {
            Hide();
        }
        if (gameStateManager.IsGameMenu())
        {
            transform.position = Vector3.zero;
        }
    }

    public void Show()
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }
}
