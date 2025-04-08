using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashCooldown = 1f;
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.2f;
    public Joystick joystick;

    public Vector2 moveDirection;
    private float nextDashTime;
    private bool isDashing;
    private bool isKnockedBack;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isPhone;
    private bool isMoving = false; // Track if the player is moving

    public bool fastDash = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D reference
        rb.gravityScale = 0; // Disable gravity to avoid unwanted movement
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isPhone = true; // it is in phone
        }
    }

    void Update()
    {
        if (!isKnockedBack) // Prevent movement during knockback
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime && moveDirection != Vector2.zero)
        {
            SoundFXManager.Instance.PlaySound("Playerdash"); // Play dash sound effect
            Dash();
        }

        // On right-click, flip the sprite (and its children, if needed) based on mouse position relative to the player.
        if (Input.GetMouseButtonDown(1))
        {
            FlipSprite();
        }
        Debug.Log("Horizontal: " + joystick.Horizontal + " | Vertical: " + joystick.Vertical);
        PlayFootStepSound(); // Call the footstep sound function
    }

    public void MovePlayer()
    {
        if (Application.isPlaying)
        {
        moveDirection = Vector2.zero;

        if (isPhone && joystick != null) 
        {
            // Use joystick input if on a phone
            moveDirection.x = joystick.Horizontal;
            moveDirection.y = joystick.Vertical; 
        }
        else
        {
            // Use keyboard input if not on a phone
            if (Input.GetKey(KeyCode.W)) moveDirection.y += 1; 
            if (Input.GetKey(KeyCode.S)) moveDirection.y -= 1; 

            if (Input.GetKey(KeyCode.D))
            {
                moveDirection.x += 1;
                FlipSpriteRight();
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection.x -= 1;
                FlipSpriteLeft();
            }
        }

        moveDirection.Normalize();

        if (!isDashing)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        }

    }


    public void Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;
        if (fastDash)
        {
            dashSpeed = 20f;
        }
        else
        {
            dashSpeed = 12f;
        }
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

        // Calculate direction away from the enemy.
        Vector2 direction = (rb.position - enemyPosition).normalized;

        // Apply knockback force in the correct direction.
        rb.linearVelocity = direction * knockbackForce;

        Invoke(nameof(EndKnockback), knockbackDuration);
    }

    void EndKnockback()
    {
        isKnockedBack = false;
        rb.linearVelocity = Vector2.zero;
    }

    // Flips the sprite based on the mouse click's position relative to the player.
    void FlipSprite()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newScale = transform.localScale;

        // If the mouse is to the right of the player, face right (flipX negative); otherwise, face left.
        if (mousePos.x > transform.position.x)
        {
            newScale.x = -Mathf.Abs(newScale.x);
        }
        else
        {
            newScale.x = Mathf.Abs(newScale.x);
        }

        transform.localScale = newScale;
    }

    void FlipSpriteRight()
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void FlipSpriteLeft()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void PlayFootStepSound()
    {
        if(isMoving && rb.linearVelocity.magnitude < 0f) // Check if the player stops moving
        {
            return;
        }
        if(!isMoving && rb.linearVelocity.magnitude > 0f) // Check if the player is moving
        {
            SoundFXManager.Instance.PlaysoundwithLoop("Playermove"); // Play footstep sound
            isMoving = true; // Set isMoving to true when the player starts moving
        }
        else if(isMoving && rb.linearVelocity.magnitude < 0.1f) // Check if the player stops moving
        {
            isMoving = false; // Reset isMoving when the player stops moving
            SoundFXManager.Instance.StopSoundEffect("Playermove"); // Stop footstep sound
        }
    }
}
