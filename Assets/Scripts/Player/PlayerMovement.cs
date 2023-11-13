using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance { get; private set; }

    private readonly float moveSpeed = 7f;

    [SerializeField] private Camera cam;
    private Rigidbody2D playerRb;
    private Vector2 mousePosition;
    private Vector2 moveDirection;

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
        moveDirection = GameInput.Instance.GetInputVectorNormalized();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
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

    private void LookAt(Vector2 lookAtDirection)
    {
        if (lookAtDirection.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
