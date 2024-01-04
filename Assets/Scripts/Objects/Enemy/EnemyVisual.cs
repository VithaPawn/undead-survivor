using PlayingObjects;
using System.Collections;
using UnityEngine;

public class EnemyVisual : MonoBehaviour {
    #region Constants
    private const string DOES_HITTED = "DoesHitted";
    private const string DOES_DIED = "DoesDied";
    #endregion Constants

    #region Variables
    private Enemy enemy;
    private Animator animator;
    #endregion Variables

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnEnable()
    {
        enemy.OnHitted += Enemy_OnHitted;
        enemy.OnDie += Enemy_OnDie;
    }

    private void OnDisable()
    {
        enemy.OnHitted -= Enemy_OnHitted;
        enemy.OnDie -= Enemy_OnDie;
    }

    private void Enemy_OnDie(object sender, System.EventArgs e)
    {
        animator.SetBool(DOES_DIED, true);
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
        animator.SetBool(DOES_HITTED, true);

        // Wait for a certain duration (e.g., 1 second)
        yield return new WaitForSeconds(0.1f);

        // Set the "IsHitting" parameter back to false
        animator.SetBool(DOES_HITTED, false);
    }
}
