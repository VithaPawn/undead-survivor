using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardSingle : MonoBehaviour {
    #region Variables
    private UpgradeData upgradeData;
    [Header("Children UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI information;
    [SerializeField] private Transform levelColection;
    [SerializeField] private Transform levelIconTemplate;
    [Header("Background")]
    [SerializeField] private Sprite defaultBackground;
    [SerializeField] private Sprite selectedBackground;
    [Header("Disable Elements")]
    [SerializeField] private Color backgroundColor;
    private Image backgroud;
    private Button button;
    #endregion Variables

    #region Events
    public event Action<UpgradeData> OnSelect;
    #endregion Events

    #region Methods
    private void Awake()
    {
        button = GetComponent<Button>();
        backgroud = GetComponent<Image>();
        if (backgroud == null || button == null)
        {
            Debug.LogError("There is no Image or Button component is attached to UpgradeCardSingle.");
        }
    }

    private void OnEnable()
    {
        button.onClick.AddListener(SelectCard);

    }

    private void OnDisable()
    {
        upgradeData = null;
        button.onClick.RemoveListener(SelectCard);
    }

    private void SelectCard()
    {
        if (upgradeData != null)
        {
            OnSelect?.Invoke(upgradeData);
        }
        else
        {
            Debug.Log("There is no UpgradeData in selected card.");
        }
    }

    public void SetUpgradeData(UpgradeData data)
    {
        upgradeData = data;
    }

    public UpgradeData GetUpgradeData() { return upgradeData; }

    public void ResetUpgradeData() { upgradeData = null; }

    public void UpdateGUI()
    {
        button.interactable = upgradeData;
        backgroud.sprite = defaultBackground;
        backgroud.color = upgradeData ? Color.white : backgroundColor;
        if (upgradeData)
        {
            icon.enabled = true;
            icon.sprite = upgradeData.Icon;
        }
        else
        {
            icon.enabled = false;
        }
        title.text = upgradeData ? upgradeData.UpgradeName : String.Empty;
        information.text = upgradeData ? upgradeData.Information : String.Empty;
        UpdateLevelGUI(upgradeData ? upgradeData.Level : 0);
    }

    private void UpdateLevelGUI(int level)
    {
        int length = level > levelColection.childCount ? level : levelColection.childCount;
        for (int i = 0; i < length; i++)
        {
            if (i < level)
            {
                if (levelColection.GetChild(i) != null)
                {
                    levelColection.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Transform levelIcon = Instantiate(levelIconTemplate, levelColection);
                    levelIcon.gameObject.SetActive(true);
                }
            }
            else
            {
                levelColection.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void UpdateDefaultBg() { backgroud.sprite = defaultBackground; }
    public void UpdateSelectedBg() { backgroud.sprite = selectedBackground; }
    #endregion Methods
}
