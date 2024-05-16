using UnityEngine;

public class BaseCanvas : MonoBehaviour {
    [SerializeField] private SoundChannelSO clickButtonSound;

    protected virtual void Show()
    {
        foreach (Transform childrenTransform in transform)
        {
            childrenTransform.gameObject.SetActive(true);
        }
    }
    protected virtual void Hide()
    {
        foreach (Transform childrenTransform in transform)
        {
            childrenTransform.gameObject?.SetActive(false);
        }
    }
    protected void PlayClickButtonSound(Vector3 buttonPosition)
    {
        clickButtonSound.RaiseEvent(buttonPosition);
    }
}
