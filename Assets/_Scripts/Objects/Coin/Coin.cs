using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable {

    #region Constants
    private const float COLLECT_SPEED = 10f;
    #endregion Constants

    #region Variables
    [SerializeField] private FloatVariableSO experienceTotal;
    private ExpPoint expSO;
    private CoinVisual visual;
    private Rigidbody2D rb;
    private bool hasTarget;
    private Vector3 targetPosition;
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

    #region Spawn

    public static void SpawnCoin(ExpPoint expSO, Vector3 spawnPosition, Transform parentObject)
    {
        Coin newEP = Instantiate(expSO.prefab, spawnPosition, Quaternion.identity);
        newEP.Initialize(expSO);
        if (parentObject) { newEP.transform.SetParent(parentObject); }
    }

    private void Initialize(ExpPoint expSO)
    {
        this.expSO = expSO;
        visual = GetComponentInChildren<CoinVisual>();
        visual.Initialize();
    }
    #endregion Spawn

    #region Collect
    public void Collect()
    {
        experienceTotal.Increase(expSO.point);
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
    public ExpPoint getExpSo() { return expSO; }
    #endregion VariableGetters
    #endregion Methods
}
