using UnityEngine;
using System.Collections;

public class UpdMelee : MonoBehaviour
{
    // Duration of the attack animation
    [SerializeField] private float attackDuration = 0.5f;
    // The starting offset for the attack relative to the player's base position
    [SerializeField] private Vector3 attackStartOffset = new Vector3(1f, 0.5f, 0f);
    // The ending offset for the attack relative to the player's base position
    [SerializeField] private Vector3 attackEndOffset = new Vector3(1f, -0.5f, 0f);
    [SerializeField] private int damage = 1;
    [SerializeField] private AudioManager audioManager;

    // Save the player's original local position
    private Vector3 originalLocalPosition;
    // Flag to prevent overlapping attacks
    private bool isAttacking = false;
    private Collider2D weaponCollider;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        weaponCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
        weaponCollider.enabled = false;

        // Save the base local position of the hitbox
        originalLocalPosition = transform.localPosition;
        // Initialize the hitbox at the start position (relative to the player)
        transform.localPosition = originalLocalPosition + attackStartOffset;
    }

    private void Update()
    {
        // Trigger the attack when the right mouse button is pressed
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            spriteRenderer.enabled = true;
            weaponCollider.enabled = true;
            TriggerAttack();
            audioManager.PlayMeleeSound();
        }
    }

    // Start the attack routine
    public void TriggerAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        float elapsed = 0f;

        // Define start and end positions relative to the player's base position.
        Vector3 startPos = originalLocalPosition + attackStartOffset;
        Vector3 endPos = originalLocalPosition + attackEndOffset;

        // Move along a straight line from start to end over the attack duration.
        while (elapsed < attackDuration)
        {
            float t = elapsed / attackDuration;
            Vector3 pos = Vector3.Lerp(startPos, endPos, t);
            transform.localPosition = pos;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the hitbox reaches the end position.
        transform.localPosition = endPos;
        // Brief pause at the end of the swing.
        yield return new WaitForSeconds(0.1f);
        // Reset the hitbox to the starting position.
        spriteRenderer.enabled = false;
        weaponCollider.enabled = false;
        transform.localPosition = startPos;
        isAttacking = false;
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
