using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExpDropTable : MonoBehaviour {
    #region Variables
    [SerializeField] private List<ExpPoint> expPoints;
    [System.NonSerialized] private bool isInitialize = false;
    private float totalWeight;
    #endregion Variables

    #region Methods
    private void Initialize()
    {
        if (!isInitialize)
        {
            totalWeight = expPoints.Sum(item => item.weight);
            isInitialize = true;
        }
    }

    public ExpPoint GetRandomPoint()
    {
        Initialize();
        float diceRoll = Random.Range(0, totalWeight);
        foreach (var item in expPoints)
        {
            if (item.weight >= diceRoll)
            {
                return item;
            }
            diceRoll -= item.weight;
        }
        return null;
    }
    #endregion Methods

    #region ExpPoint
    [System.Serializable]
    public class ExpPoint {
        public DatabaseSystem.ScriptableObjects.ExpPoint point;
        public float weight;
    }
    #endregion ExpPoint
}
