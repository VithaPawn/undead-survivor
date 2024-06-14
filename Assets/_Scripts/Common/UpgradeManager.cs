using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance {  get; private set; }

    [Header("Managers")]
    [SerializeField] private GameStateManagerSO gameStateManager;

    [Header("Upgrade Pool")]
    [SerializeField] private UpgradePool pool;
    private Dictionary<int, UpgradeData> playingUpgradeSystemDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogError("There are more than one UpgradeManager at the same time!");
        }

        playingUpgradeSystemDict = new Dictionary<int, UpgradeData>();
    }

    private void OnEnable()
    {
        gameStateManager.OnChanged += GameStateManager_OnChanged;
        pool.OnUpgrade += Pool_OnUpgrade;
    }


    private void OnDisable()
    {
        gameStateManager.OnChanged -= GameStateManager_OnChanged;
    }

    private void GameStateManager_OnChanged()
    {
        if (gameStateManager.IsGamePlaying())
        {
            playingUpgradeSystemDict.Clear();
        }
    }
    private void Pool_OnUpgrade(UpgradeData obj)
    {
        if (playingUpgradeSystemDict.ContainsKey(obj.UpgradeSystemId))
        {
            playingUpgradeSystemDict[obj.UpgradeSystemId] = obj;
        } else
        {
            playingUpgradeSystemDict.Add(obj.UpgradeSystemId, obj);
        }
    }

    public Dictionary<int, UpgradeData> GetPlayingUpgradeSystemDict() { return playingUpgradeSystemDict; }
}
