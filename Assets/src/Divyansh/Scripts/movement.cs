using UnityEngine;

public class Movement : MonoBehaviour
{
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();  // Get Animator
    }

    void Update()
    {
        // Get Player Input (A/D or Left/Right Arrows)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Update the Animator Blend Tree Parameter
        animator.SetFloat("xVelocity", Mathf.Abs(movement.x));
    }
}
