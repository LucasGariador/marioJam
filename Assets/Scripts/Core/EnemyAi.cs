using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Chase, Idle }
    public EnemyState currentState = EnemyState.Idle;

    private GameManager.FoodType currentFoodType;
    private GameObject currentFoodSprite;
    [SerializeField]
    private Transform foodPosition;

    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad del enemigo
    public float stopDistance = 1.5f; // Distancia mínima antes de detenerse
    public float idleTime = 3f; // Tiempo que permanece en estado Idle
    public Sprite idleSprite; // Sprite para el modo Idle

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Sprite originalSprite; // Sprite original para el modo Chase
    private float idleTimer = 0f;

    void Start()
    {

        int foodTypes = Enum.GetNames(typeof(GameManager.FoodType)).Length;
        int randomFoodItem = UnityEngine.Random.Range(0, foodTypes);
        currentFoodType = (GameManager.FoodType)randomFoodItem;
        currentFoodSprite = GameManager.instance.GetFoodSprite(currentFoodType);
        Instantiate(currentFoodSprite, foodPosition.position, Quaternion.identity, foodPosition);

        Debug.Log("My food is "+ currentFoodType);
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; // Guarda el sprite original
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                foodPosition.gameObject.SetActive(true);
                ChasePlayer();
                break;

            case EnemyState.Idle:
                foodPosition.gameObject.SetActive(false);
                Idle();
                break;
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Opcional: Ajusta la rotación del enemigo hacia el jugador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Idle()
    {
        rb.linearVelocity = Vector2.zero; // Detén el movimiento
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0)
        {
            // Cambia de vuelta al estado de persecución
            spriteRenderer.sprite = originalSprite;
            //currentState = EnemyState.Chase;
        }
    }

    public void GetHit(GameManager.FoodType FoodTypeHit)
    {
        // Cambia al estado Idle
        currentState = EnemyState.Idle;
        spriteRenderer.sprite = idleSprite; // Cambia el sprite
        idleTimer = idleTime; // Reinicia el temporizador
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
