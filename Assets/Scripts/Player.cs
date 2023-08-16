using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Rigidbody2D playerRb;

    private float moveSpeed = 7f;
    private Vector2 moveDirection;

    //private void Update()
    //{
    //    moveDirection.x = Input.GetAxisRaw("Horizontal");
    //    moveDirection.y = Input.GetAxisRaw("Vertical");
    //}

    private void FixedUpdate()
    {
        moveDirection = GameInput.Instance.GetInputVectorNormalized();
        playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
