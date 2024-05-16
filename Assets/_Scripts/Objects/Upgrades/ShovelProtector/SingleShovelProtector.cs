using UnityEngine;

public class SingleShovelProtector : MonoBehaviour, IDamageObject {
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Transform player;

    public float Damage { get; set; } = 40f;

    private void Update()
    {
        transform.RotateAround(player.position, Vector3.back, rotateSpeed * Time.deltaTime);
    }
}
