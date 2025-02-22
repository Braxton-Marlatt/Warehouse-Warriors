using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 5;
    // Increase swingSpeed for a faster swing (degrees per second)
    [SerializeField] private float swingSpeed = 750;      
    // Set attackAngle to 180° so the sword rotates from 90° to 270°
    [SerializeField] private float attackAngle = 180f;    

    private Collider2D weaponCollider;
    private bool isAttacking = false;
    private float startAngle;
    private float endAngle;
    private float swingTime;
    private Transform weaponTransform;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        weaponTransform = transform;
        weaponCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Hide the sword when not in motion
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (weaponCollider != null)
            weaponCollider.enabled = false;

        // Set the starting angle to 270 (pointing straight up)
        startAngle = 270f;
        // The sword swings downward to 150° (top down swing)
        endAngle = 150f;
        
        // Ensure the sword starts at the top (90°)
        weaponTransform.localEulerAngles = new Vector3(0, 0, startAngle);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Swing();
        }
    }

    void Swing()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            swingTime = 0f;
            if (spriteRenderer != null)
                spriteRenderer.enabled = true; // Make the sword visible during the swing
            if (weaponCollider != null)
                weaponCollider.enabled = true; // Enable collider for hit detection

            StartCoroutine(PerformSwing());
        }
    }

    private IEnumerator PerformSwing()
    {
        // Calculate how long the swing should take based on the angle and speed
        float duration = attackAngle / swingSpeed;
        while (swingTime < duration)
        {
            swingTime += Time.deltaTime;
            float angle = Mathf.Lerp(startAngle, endAngle, swingTime / duration);
            weaponTransform.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        // Reset the sword after swinging
        if (weaponCollider != null)
            weaponCollider.enabled = false;
        if (spriteRenderer != null)
            spriteRenderer.enabled = false; // Hide the sword again

        isAttacking = false;
        // Reset the sword to its starting angle (pointing up)
        weaponTransform.localEulerAngles = new Vector3(0, 0, startAngle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.Hurt(damage);
            }
        }
    }
}
