using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelCanvas : BaseCanvas {
    #region Constants
    private const int UPGRADE_CARD_NUMBER = 3;
    #endregion Constants

    #region Variables
    [SerializeField] private FloatVariableSO currentLevel;
    [SerializeField] private UpgradePool upgradePool;
    [Header("Children UIs")]
    [SerializeField] private Button chooseBtn;
    #endregion Variables

    #region Methods
    private void OnEnable()
    {
        currentLevel.OnChanged += CurrentLevel_OnChanged;
    }

    private void OnDisable()
    {
        currentLevel.OnChanged -= CurrentLevel_OnChanged;
    }

    private void Start()
    {
        chooseBtn.onClick.AddListener(() => { Hide(); });
        Hide();
    }

    private void CurrentLevel_OnChanged()
    {
        Show();
    }

    private void UpdateUpgradeCards()
    {
        List<UpgradeData> upgradeDatas = upgradePool.GetRandomAvaiableUpgrades(UPGRADE_CARD_NUMBER);
    }
    #endregion Methods
}
