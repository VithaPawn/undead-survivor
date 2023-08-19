using System.Collections;
using UnityEngine;

public class EnemyVisual : MonoBehaviour {
    private const string IS_HITTING = "IsHitting";
    private const string IS_DIE = "IsDie";

    [SerializeField] private Enemy enemy;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        enemy.OnHitted += Enemy_OnHitted;
        enemy.OnDie += Enemy_OnDie;
    }

    private void Enemy_OnDie(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
    }

    private void Enemy_OnHitted(object sender, System.EventArgs e)
    {
        StartHittingAnimation();
    }

    public void StartHittingAnimation()
    {
        StartCoroutine(PlayHittingAnimation());
    }

    private IEnumerator PlayHittingAnimation()
    {
        // Set the "IsHitting" parameter to true to trigger the animation
        animator.SetBool(IS_HITTING, true);

        // Wait for a certain duration (e.g., 1 second)
        yield return new WaitForSeconds(0.1f);

        // Set the "IsHitting" parameter back to false
        animator.SetBool(IS_HITTING, false);
    }
}
