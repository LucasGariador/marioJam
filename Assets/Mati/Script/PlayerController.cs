using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private float currentSpeed;
    private float speedMultiplier = 0.01f;
    private Vector2 moveInput;
    [SerializeField] private AudioClip clip;

    [Header("Sprites por dirección")]
    [SerializeField] private Sprite spriteUp;
    [SerializeField] private Sprite spriteDown;
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY);

        // Cambiar el sprite dependiendo de la dirección
        UpdateSpriteDirection(moveX, moveY);
    }

    private void UpdateSpriteDirection(float moveX, float moveY)
    {
        if (moveX > 0) // Movimiento hacia la derecha
        {
            spriteRenderer.sprite = spriteRight;
        }
        else if (moveX < 0) // Movimiento hacia la izquierda
        {
            spriteRenderer.sprite = spriteLeft;
        }
        else if (moveY > 0) // Movimiento hacia arriba
        {
            spriteRenderer.sprite = spriteUp;
        }
        else if (moveY < 0) // Movimiento hacia abajo
        {
            spriteRenderer.sprite = spriteDown;
        }
    }

    public void SlowDown()
    {
        speedMultiplier /= 2;
    }
    public void NormalSpeed()
    {
        speedMultiplier = 0.01f;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + speedMultiplier * speed * moveInput);
    }
}
