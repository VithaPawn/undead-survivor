using UnityEngine;
using UnityEngine.UI;

public class UpgradeIconSingle : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private Transform starsTranform;

    public void UpdateGUI(UpgradeData upgradeData)
    {
        if (upgradeData == null) return;
        iconImage.sprite = upgradeData.Icon;
        for (int i = 0; i < starsTranform.childCount; i++)
        {
            Transform star = starsTranform.GetChild(i);
            if (i < starsTranform.childCount - upgradeData.Level)
            {
                star.gameObject.SetActive(false);
            } else
            {
                star.gameObject.SetActive(true);
            }
        }
    }
}
