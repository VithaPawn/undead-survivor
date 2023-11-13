using UnityEngine;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject {
    public Enemy enemyPrefab;
    public string enemyName;
    public float damageToPlayer;
    public float health;
    public float speed;
    public float bodyRemainDelay;
    public float value;
    public int score;
}
