using UnityEngine;

public class SingleShovelProtector : MonoBehaviour, IDamageObject {
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Transform rotateCenterTransform;

    public float Damage { get; set; } = 40f;

    private void Update()
    {
        transform.RotateAround(rotateCenterTransform.position, Vector3.back, rotateSpeed * Time.deltaTime);
    }

    public void SetRotateCenterTransform(Transform transform)
    {
        rotateCenterTransform = transform;
    }
}
