using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Chase, Idle }
    public EnemyState currentState = EnemyState.Idle;
    private bool isFull = false;

    private GameManager.FoodType currentFoodType;
    private GameObject currentFoodSprite;
    [SerializeField]
    private Transform foodPosition;
    [SerializeField]
    float agroDistance = 15;
    [SerializeField]
    private GameObject mapViewer;
    
    public Transform player; // Referencia al jugador
    public float speed = 3f; // Velocidad del enemigo
    private float currentSpeed;
    public float stopDistance = 1.5f; // Distancia mínima antes de detenerse
    public Sprite idleSprite; // Sprite para el modo Idle

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Sprite originalSprite; // Sprite original para el modo Chase

    [SerializeField] private AudioClip[] audioCruch;

    void Start()
    {
        Debug.Log("My food is "+ currentFoodType);
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite; // Guarda el sprite original
        currentSpeed = speed;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                if(foodPosition.childCount == 0)
                Instantiate(GetRandomFoodObject(), foodPosition.position, Quaternion.identity, foodPosition);

                foodPosition.gameObject.SetActive(true);
                ChasePlayer();
                break;

            case EnemyState.Idle:
                if (foodPosition.childCount >= 1)
                    Destroy(foodPosition.transform.GetChild(0).gameObject);

                currentSpeed = speed;
                foodPosition.gameObject.SetActive(false);
                Idle();
                break;
        }
    }

    private GameObject GetRandomFoodObject()
    {
        int foodTypes = Enum.GetNames(typeof(GameManager.FoodType)).Length;
        int randomFoodItem = UnityEngine.Random.Range(0, foodTypes);
        currentFoodType = (GameManager.FoodType)randomFoodItem;
        currentFoodSprite = GameManager.instance.GetFoodSprite(currentFoodType);
        return currentFoodSprite;
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            rb.linearVelocity = direction * currentSpeed;
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
        if(Vector2.Distance(transform.position, GameManager.instance.playerPosition.position) <= agroDistance && !isFull)
        {
            currentState = EnemyState.Chase;
        }
    }

    void SoundCrunch()
    {
        if (audioCruch.Length == 0)
        {
            Debug.LogWarning("La lista de sonidos está vacía.");
            return;
        }

        // Elije un índice aleatorio
        int indiceAleatorio = UnityEngine.Random.Range(0, audioCruch.Length);

        AudioManager.instance.PlaySFX(audioCruch[indiceAleatorio]);
    }

    public void GetHit(GameManager.FoodType foodTypeHit)
    {
        // Cambia al estado Idle
        if (foodTypeHit == currentFoodType)
        {
            SoundCrunch();
            currentState = EnemyState.Idle;
            isFull = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false; //Desactiva el collider
            mapViewer.SetActive(false);

        }
        else
        {
            currentSpeed += speed/2;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.LoseGame();
            Invoke("Reiniciar", 2f);
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Lucas");
    }
}
