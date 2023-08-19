using UnityEngine;

public class PlayerVisual : MonoBehaviour {

    private const string SPEED = "Speed";
    private const string IS_WALK_BACKWARD = "IsWalkBackward";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player.OnMove += Player_OnMove;
    }

    private void Player_OnMove(object sender, Player.OnMoveEventArgs e)
    {
        animator.SetFloat(SPEED, e.moveDirection.sqrMagnitude);
        animator.SetBool(IS_WALK_BACKWARD, e.isWalkBackward);
    }
}
