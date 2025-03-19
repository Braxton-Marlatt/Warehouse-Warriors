using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D
        animator = GetComponent<Animator>();  // Get Animator
    }

    void Update()
    {
        // Get Player Input (A/D or Left/Right Arrows)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Update the Animator Blend Tree Parameter
        animator.SetFloat("xVelocity", Mathf.Abs(movement.x));
    }

    void FixedUpdate()
    {
        // Move the Player
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);
    }
}
