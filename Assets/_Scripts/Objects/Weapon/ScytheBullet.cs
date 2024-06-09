using UnityEngine;

public class ScytheBullet : Bullet {
    const float rotationSpeed = 1000f;
    private bool isRotate = true;

    private void OnEnable()
    {
        isRotate = true;
    }

    private void OnDisable()
    {
        isRotate = false;
    }

    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
    }
}
