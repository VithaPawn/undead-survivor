using UnityEngine;

public class PositionRendererSorter : MonoBehaviour {

    #region Constants
    const int SORTING_LAYER_MULTIPLIER = -1000;
    #endregion Constants

    #region Variables
    [SerializeField] private int offset;
    [SerializeField] private bool runOnlyOnce = false;
    [SerializeField] private bool runEachTimeEnable = false;
    private bool isSorted = false;
    private Renderer myRenderer;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        isSorted = true;
    }

    private void LateUpdate()
    {
        if (isSorted)
        {
            SortRender();
            if (runEachTimeEnable ) { isSorted = false; }
        }
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }

    private void SortRender()
    {
        myRenderer.sortingOrder = (int)(transform.position.y * SORTING_LAYER_MULTIPLIER + offset);
    }
    #endregion Methods
}
