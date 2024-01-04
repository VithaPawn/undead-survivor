using UnityEngine;

public class PositionRendererSorter : MonoBehaviour {

    #region Constants
    const int SORTING_LAYER_MULTIPLIER = -1000;
    #endregion Constants

    #region Variables
    [SerializeField] private int offset;
    [SerializeField] private bool runOnlyOnce = false;
    private Renderer myRenderer;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)((transform.position.y + offset) * SORTING_LAYER_MULTIPLIER);
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
    #endregion Methods
}
