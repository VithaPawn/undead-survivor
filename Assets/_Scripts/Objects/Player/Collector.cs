using UnityEngine;

public class Collector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}
