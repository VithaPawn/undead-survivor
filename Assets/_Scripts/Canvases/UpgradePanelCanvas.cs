using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelCanvas : BaseCanvas {
    #region Variables
    [SerializeField] private FloatVariableSO currentLevel;
    [Header("Upgrade Parameters")]
    [SerializeField] private UpgradePool upgradePool;
    [SerializeField] private int upgradeCardNumber = 3;
    [Header("Children UIs")]
    [SerializeField] private Transform upgradeMenu;
    [SerializeField] private Transform upgradeCardTemplate;
    [SerializeField] private Button selectedBtn;

    private UpgradeData choosedUpgradeData = null;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        upgradeCardTemplate.gameObject.SetActive(false);
        UpdateUpgradeCardNumber(upgradeCardNumber);
    }

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
        selectedBtn.onClick.AddListener(() => { Hide(); upgradePool.AddToApplyingList(choosedUpgradeData); });
        Hide();
    }

    private void CurrentLevel_OnChanged()
    {
        Show();
        UpdateUpgradeMenu();
    }

    private void UpdateUpgradeMenu()
    {
        List<UpgradeData> upgradeDatas = upgradePool.GetRandomAvailableUpgrades(upgradeCardNumber);
        for (int i = 0; i < upgradeCardNumber; i++)
        {
            if (upgradeMenu.GetChild(i).TryGetComponent(out UpgradeCardSingle upgradeCard))
            {
                upgradeCard.gameObject.SetActive(true);
                if (i < upgradeDatas.Count && upgradeDatas[i])
                {
                    upgradeCard.SetUpgradeData(upgradeDatas[i]);
                    upgradeCard.OnSelect += UpgradeCard_OnSelect;
                }
                upgradeCard.UpdateGUI();
            }
        }
        UpdateBtnStatus(selectedBtn, false);
    }

    private void UpgradeCard_OnSelect(UpgradeData data)
    {
        choosedUpgradeData = data;
        foreach (Transform child in upgradeMenu)
        {
            if (child.TryGetComponent(out UpgradeCardSingle upgradeCard))
            {
                if (data == upgradeCard.GetUpgradeData())
                {
                    upgradeCard.UpdateSelectedBg();
                }
                else
                {
                    upgradeCard.UpdateDefaultBg();
                }
            }
        }
        UpdateBtnStatus(selectedBtn, true);
    }

    private void UpdateUpgradeCardNumber(int cardNumber)
    {
        foreach (Transform child in upgradeMenu)
        {
            if (child != upgradeCardTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        if (cardNumber == 0)
        {
            Debug.LogError("There is no UpgradeData item in the pool.");
            return;
        }
        for (int i = 0; i < cardNumber - 1; i++)
        {
            Instantiate(upgradeCardTemplate, upgradeMenu);
        }
    }

    private void UpdateBtnStatus(Button btn, bool isSelected)
    {
        Image imgBtn = btn.GetComponent<Image>();
        if (imgBtn == null) { return; }

        // Set the normalColor based on the isSelected parameter
        imgBtn.color = isSelected ? Color.white : Color.gray;
        btn.enabled = isSelected;
    }

    protected override void Show()
    {
        Time.timeScale = 0f;
        base.Show();
    }

    protected override void Hide()
    {
        Time.timeScale = 1f;
        base.Hide();
    }
    #endregion Methods
}
