using DatabaseSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class Coin : MonoBehaviour, ICollectable {

    #region Constants
    private const float COLLECT_SPEED = 10f;
    #endregion Constants

    #region Variables
    [SerializeField] private FloatVariableSO experienceTotal;
    private ExpPoint coinSO;
    private Rigidbody2D rb;
    private bool hasTarget;
    private Vector3 targetPosition;
    [Header("Parent Coin Pool")]
    private IObjectPool<Coin> coinPool;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hasTarget = false;
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            MoveToTarget();
        }
    }

    public IObjectPool<Coin> CoinPool { set { coinPool = value; } }

    public void SpawnCoin(ExpPoint coinData, Vector3 spawnPosition)
    {
        coinSO = coinData;
        CoinVisual visual = GetComponentInChildren<CoinVisual>();
        visual.Initialize();
    }

    #region Collect
    public void Collect()
    {
        experienceTotal.Increase(coinSO.point);
        Destroy(gameObject);
    }

    private void MoveToTarget()
    {
        Vector2 targetDirection = (targetPosition - transform.position).normalized;
        rb.velocity = targetDirection * COLLECT_SPEED;
    }

    public void SetTarget(Vector3 position)
    {
        hasTarget = true;
        targetPosition = position;
    }

    #endregion Collect

    #region VariableGetters
    public ExpPoint getCoinSO() { return coinSO; }
    #endregion VariableGetters
    #endregion Methods
}
