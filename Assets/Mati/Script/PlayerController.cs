using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private float currentSpeed;
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

        if(moveX >0 && moveY > 0)
        {
            currentSpeed = speed / 2;
        }
        else
        {
            currentSpeed = speed;
        }

        moveInput = new Vector2(moveX, moveY);

    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveInput * speed * 0.01f);   
    }
}
