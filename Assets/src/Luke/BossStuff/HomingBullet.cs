using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float rotationSpeed;
    private int damage;
    private Rigidbody2D rb;

    public void Initialize(float speed, float rotationSpeed, int damage)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.damage = damage;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Hurt(damage);
            Destroy(gameObject);
        }
    }
}