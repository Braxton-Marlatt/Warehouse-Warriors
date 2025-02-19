using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashCooldown = 1f;
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.2f;

    private Vector2 moveDirection;
    private float nextDashTime;
    private bool isDashing;
    private bool isKnockedBack;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D reference
        rb.gravityScale = 0; // Disable gravity to avoid unwanted movement
    }

    void Update()
    {
        if (!isKnockedBack) // Prevent movement during knockback
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime && moveDirection != Vector2.zero)
        {
            Dash();
        }
    }

    public void MovePlayer()
    {
        moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) moveDirection.y += 1;
        if (Input.GetKey(KeyCode.S)) moveDirection.y -= 1;
        if (Input.GetKey(KeyCode.D)) moveDirection.x += 1;
        if (Input.GetKey(KeyCode.A)) moveDirection.x -= 1;

        moveDirection.Normalize();

        if (!isDashing)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    void Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;
        rb.linearVelocity = moveDirection * dashSpeed;
        Invoke(nameof(EndDash), 0.1f);
    }

    void EndDash()
    {
        isDashing = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Knockback(collision.transform.position);
        }
    }

    public void Knockback(Vector2 knockbackDirection)
    {
        isKnockedBack = true;
        rb.linearVelocity = knockbackDirection * knockbackForce;

        Invoke(nameof(EndKnockback), knockbackDuration);
    }

    void EndKnockback()
    {
        isKnockedBack = false;
        rb.linearVelocity = Vector2.zero;
    }
}
