using DatabaseSystem.ScriptableObjects;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour {
    private ExpPoint expSO;
    private ExperiencePointVisual visual;

    #region Spawn
    public static void SpawnExperiencePoint(ExpPoint expSO, Vector3 spawnPosition)
    {
        ExperiencePoint newEP = Instantiate(expSO.prefab, spawnPosition, Quaternion.identity);
        newEP.Initialize(expSO);
    }

    private void Initialize(ExpPoint expSO)
    {
        this.expSO = expSO;
        visual = GetComponentInChildren<ExperiencePointVisual>();
        visual.Initialize();
    }
    #endregion Spawn

    #region VariableGetters
    public ExpPoint getExpSo() { return expSO; }
    #endregion VariableGetters
}
