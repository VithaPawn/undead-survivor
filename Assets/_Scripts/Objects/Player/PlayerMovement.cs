using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; }

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [Header("Movement Attributes")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private BooleanVariableSO isPlayerTurnRight;
    private Rigidbody2D playerRb;
    private Vector2 mousePosition;
    private Vector2 moveDirection;
    [Header("Game State Manager")]
    [SerializeField] private GameStateManagerSO gameStateManagerSO;
    [Header("Player Health")]
    [SerializeField] private FloatVariableSO playerHealth;

    public event EventHandler<OnMoveEventArgs> OnMove;
    public class OnMoveEventArgs : EventArgs {
        public Vector2 moveDirection;
        public float WalkVisualDirection;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance!");
        }
        else
        {
            Instance = this;
        }
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (gameStateManagerSO.IsGamePlaying())
        {
            moveDirection = GameInput.Instance.GetInputVectorNormalized();
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (gameStateManagerSO.IsGamePlaying() && playerHealth.GetValue() > 0)
        {
            // Change player position
            playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Handle what side that player looks at
            Vector2 lookAtDirection = mousePosition - playerRb.position;
            LookAt(lookAtDirection);

            // Fire movement event to player visual
            Vector2 lookAtDirectionNormalized = lookAtDirection.normalized;
            OnMove?.Invoke(this, new OnMoveEventArgs
            {
                moveDirection = moveDirection,
                WalkVisualDirection = lookAtDirectionNormalized.x * moveDirection.x
            });
        }
    }

    private void LookAt(Vector2 lookAtDirection)
    {
        if (lookAtDirection.x >= 0)
        {
            isPlayerTurnRight.SetValue(true);
        }
        else
        {
            isPlayerTurnRight.SetValue(false);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}
