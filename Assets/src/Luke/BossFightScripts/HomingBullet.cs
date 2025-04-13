// Description: This script controls the behavior of a homing bullet in a 2D game. The bullet follows the player and deals damage upon collision.

using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float rotationSpeed;
    public int damage;
    public Rigidbody2D rb;

    public virtual void Initialize(float speed, float rotationSpeed, int damage) // Function to initialize the bullet's speed, rotation speed, and damage
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.damage = damage;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() // Update is called once per frame
    {
        if (!target) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotationSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other) // Function to handle collision with other objects
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Hurt(damage);
            Destroy(gameObject);
        }
    }
}