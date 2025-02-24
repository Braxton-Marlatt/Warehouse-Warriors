using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private enum PlayerState { Idle, Running, Jumping, Dying }
    private PlayerState currentState;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ChangeState(PlayerState.Idle);
    }

    void Update()
    {
        HandleState();
    }

    void HandleState()
    {
        if (currentState == PlayerState.Dying) return; // Prevent changes after death

        if (rb.velocity.y > 0.1f) // Checks if the player is moving upwards
        {
            ChangeState(PlayerState.Jumping);
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            ChangeState(PlayerState.Running);
        }
        else
        {
            ChangeState(PlayerState.Idle);
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        switch (newState)
        {
            case PlayerState.Idle:
                animator.Play("Idle");
                break;
            case PlayerState.Running:
                animator.Play("Run");
                break;
            case PlayerState.Jumping:
                animator.Play("Jump");
                break;
            case PlayerState.Dying:
                animator.Play("Death");
                break;
        }
    }

    public void TriggerDeath()
    {
        ChangeState(PlayerState.Dying);
    }
}
