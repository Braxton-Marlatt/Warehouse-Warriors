using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashCooldown = 1f;

    private Vector3 moveDirection;
    private float nextDashTime;
    private bool isDashing;

    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime && moveDirection != Vector3.zero)
        {
            Dash();
        }
    }

    public void MovePlayer()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) 
        {
            moveDirection.y += 1;
        }
        
        if (Input.GetKey(KeyCode.S)) 
        {
            moveDirection.y -= 1;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }

        moveDirection.Normalize();

        // Apply movement
        float speed;
        if (isDashing)
        {
            speed = dashSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;
        Invoke("EndDash", 0.1f); // Short dash duration
    }

    void EndDash()
    {
        isDashing = false;
    }
}
