using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad del enemigo
    public float stopDistance = 1.5f; // Distancia mínima antes de detenerse

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return; // Asegúrate de que hay un jugador

        // Calcula la dirección hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Comprueba la distancia al jugador
        float distance = Vector2.Distance(transform.position, player.position);

        // Si la distancia es mayor que el rango mínimo, mueve al enemigo
        if (distance > stopDistance)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Detener el movimiento si está cerca
        }

        // Opcional: Rota el enemigo para mirar hacia el jugador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detención en la vista de escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
