using System;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Camera cam;

    private readonly float moveSpeed = 7f;
    private Vector2 mousePosition;
    private Vector2 lookAtDirection;
    private Vector2 moveDirection;
    private bool IsWalkBackward;

    public event EventHandler<OnMoveEventArgs> OnMove;
    public class OnMoveEventArgs : EventArgs {
        public Vector2 moveDirection;
        public bool isWalkBackward;
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
        lookAtDirection = mousePosition - playerRb.position;
        if (lookAtDirection.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (lookAtDirection.x * moveDirection.x >= 0)
        {
            IsWalkBackward = false;
        }
        else
        {
            IsWalkBackward = true;
        }

        OnMove?.Invoke(this, new OnMoveEventArgs
        {
            moveDirection = moveDirection,
            isWalkBackward = IsWalkBackward
        });
    }
}
