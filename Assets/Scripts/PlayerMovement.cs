using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private Vector2 knockbackVelocity;
    private float knockbackTimer = 0f;
    public float knockbackForce = 7f;
    public float knockbackDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        // Change what is considered "Horizontal" & "Vertical" in the project manager
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void FixedUpdate(){
        if (knockbackTimer > 0){
            knockbackTimer -= Time.fixedDeltaTime; // Add knockback
            rb.linearVelocity = knockbackVelocity;
        }else{
            rb.linearVelocity = moveDirection * moveSpeed; // Regular movement
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        knockbackVelocity = direction.normalized * knockbackForce;
        knockbackTimer = knockbackDuration;
    }
}