using UnityEngine;

public class SpawnArea : MonoBehaviour {
    #region Constants
    #endregion Constants

    #region Variables
    [SerializeField] private Vector2 SpawnAreaValue;
    [SerializeField] private Vector2 limitArea;
    #endregion Variables

    #region Methods

    public Vector3 GetRelativePawnPositionByLandmark(Vector3 landmarkPosition)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = landmarkPosition + GenerateAbsoluteSpawnPosition();
        } while (Mathf.Abs(spawnPosition.x) >= limitArea.x || Mathf.Abs(spawnPosition.y) >= limitArea.y);

        return spawnPosition;
    }

    private Vector3 GenerateAbsoluteSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float fixedParameter = GetRandomBoolean() ? 1f : -1f;
        if (GetRandomBoolean())
        {
            spawnPosition.x = Random.Range(-SpawnAreaValue.x, SpawnAreaValue.x);
            spawnPosition.y = SpawnAreaValue.y * fixedParameter;
        }
        else
        {
            spawnPosition.x = SpawnAreaValue.x * fixedParameter;
            spawnPosition.y = Random.Range(-SpawnAreaValue.y, SpawnAreaValue.y);
        }
        return spawnPosition;
    }

    private bool GetRandomBoolean()
    {
        if (Random.value > 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion Methods
}
