
using UnityEngine;

public class MovePlayerScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector3 velocity = Vector3.zero;
    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public SpriteRenderer spriteRenderer;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position,groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if ((Input.GetButtonDown("Jump")) && (isGrounded))
        {
            isJumping = true;
        }
        MovePlayer(horizontalMovement);
        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void MovePlayer (float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y); 
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;  
        }
        
    }
    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}

