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
    private Rigidbody2D rb;
    private ExpPoint coinSO;
    private bool hasTarget;
    private Vector3 targetPosition;
    [Header("Parent Coin Pool")]
    private IObjectPool<Coin> coinPool;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        hasTarget = false;
        coinSO = null;
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            MoveToTarget();
        }
    }

    public IObjectPool<Coin> CoinPool { set { coinPool = value; } }

    public void SetCoinValue(ExpPoint coinData)
    {
        coinSO = coinData;
        CoinVisual visual = GetComponentInChildren<CoinVisual>();
        visual.Initialize();
    }

    public void Collect()
    {
        experienceTotal.Increase(coinSO.point);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        coinPool.Release(this);
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

    public ExpPoint getCoinSO() { return coinSO; }

    #endregion Methods
}
