using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Ajustes")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private string playerTag = "Player";

    private void Update()
    {
        // Hace rotar la moneda
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin();
            }

            // Destruye la moneda recogida al contacto
            Destroy(gameObject);
        }
    }
}