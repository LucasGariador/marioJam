using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private Vector2 moveInput;
    [SerializeField] AudioClip clip;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY);

    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveInput * speed * Time.deltaTime);   
    }
}
