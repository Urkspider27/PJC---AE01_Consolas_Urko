using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        // Leer valor input X , Y
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // Conversion de ejes 2D a 3D
        Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y);

        //  Fuerza pa mover el player
        rb.AddForce(movement * speed);
    }
}