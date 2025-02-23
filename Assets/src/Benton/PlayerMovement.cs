using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashCooldown = 1f;
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.2f;

    public Vector2 moveDirection;
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
        if (Application.isPlaying)
        {
            moveDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W)) moveDirection.y += 1;
            if (Input.GetKey(KeyCode.S)) moveDirection.y -= 1;
            if (Input.GetKey(KeyCode.D)) moveDirection.x += 1;
            if (Input.GetKey(KeyCode.A)) moveDirection.x -= 1;

            moveDirection.Normalize();
        }
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
            GetComponent<PlayerHealth>().Hurt();
        }
    }

    public void Knockback(Vector2 enemyPosition)
    {
        isKnockedBack = true;

        // Calculate direction away from the enemy
        Vector2 direction = (rb.position - enemyPosition).normalized;

        // Apply knockback force in the correct direction
        rb.linearVelocity = direction * knockbackForce;

        Invoke(nameof(EndKnockback), knockbackDuration);
    }

    void EndKnockback()
    {
        isKnockedBack = false;
        rb.linearVelocity = Vector2.zero;
    }
}
