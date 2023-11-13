using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    private readonly float maxHealth = 1000f;
    private float health;


    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        PlayerBehavior.Instance.OnBeHitted += PlayerBehavior_OnBeHitted;
    }

    private void PlayerBehavior_OnBeHitted(object sender, PlayerBehavior.OnBeHittedEventArgs e)
    {
        LostHealth(e.collidedEnemy);
    }

    private void LostHealth(Enemy enemy)
    {
        health -= enemy.GetEnemyDamageToPlayer();
        Debug.Log(health);
    }
}
