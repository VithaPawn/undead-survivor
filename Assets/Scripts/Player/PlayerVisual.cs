using System.Collections;
using UnityEngine;

public class PlayerVisual : MonoBehaviour {

    private const string SPEED = "Speed";
    private const string WALK_VISUAL_DIRECTION = "WalkVisualDirection";
    private const string IS_HITTED = "IsHitted";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerMovement.Instance.OnMove += Player_OnMove;
        PlayerBehavior.Instance.OnBeHitted += PlayerBehavior_OnBeHitted;
    }

    private void PlayerBehavior_OnBeHitted(object sender, PlayerBehavior.OnBeHittedEventArgs e)
    {
        PlayHittedAnimation();
    }

    private void Player_OnMove(object sender, PlayerMovement.OnMoveEventArgs e)
    {
        animator.SetFloat(SPEED, e.moveDirection.sqrMagnitude);
        animator.SetFloat(WALK_VISUAL_DIRECTION, e.WalkVisualDirection);
    }

    public void PlayHittedAnimation()
    {
        StartCoroutine(PlayHittedAnimationCoroutine());
    }

    private IEnumerator PlayHittedAnimationCoroutine()
    {
        // Set the "IsHitting" parameter to true to trigger the animation
        SpriteRenderer spriteRenderer;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = Color.red;

        // Wait for a certain duration (e.g., 1 second)
        yield return new WaitForSeconds(0.1f);

        // Set the "IsHitting" parameter back to false
        spriteRenderer.color = Color.white;
    }

}
