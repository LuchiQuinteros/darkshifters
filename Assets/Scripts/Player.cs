using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Velocidad de movimietno")]
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    [Tooltip("Velocidad de rotacion")]
    public float rotateSpeed = 10f; // Velocidad de rotación del personaje
    private Rigidbody rb; // Componente Rigidbody del personaje

    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        #region Basic directional movement
        // Obtener la entrada del usuario
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Si el personaje se está moviendo, rotar en la dirección del movimiento
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime));
        }

        // Mover el personaje en la dirección del movimiento
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        #endregion
    }
}

