using UnityEngine;

public class CoinVisual : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private Coin _experiencePoint;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _experiencePoint = GetComponentInParent<Coin>();
    }

    public void Initialize()
    {
        SetSprite(_experiencePoint.getCoinSO().sprite);
    }

    private void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}
