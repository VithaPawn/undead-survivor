using UnityEngine;

public class ExperiencePointVisual : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private ExperiencePoint _experiencePoint;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _experiencePoint = GetComponentInParent<ExperiencePoint>();
    }

    public void Initialize()
    {
        SetSprite(_experiencePoint.getExpSo().sprite);
    }

    private void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
