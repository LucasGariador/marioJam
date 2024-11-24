using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private float currentSpeed;
    private float speedMultiplier = 0.01f;
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

        //if (moveX != 0 && moveY != 0)
        //{
        //    currentSpeed = speed / 2;
        //}
        //else
        //{
        //    currentSpeed = speed;
        //}

        moveInput = new Vector2(moveX, moveY);

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
