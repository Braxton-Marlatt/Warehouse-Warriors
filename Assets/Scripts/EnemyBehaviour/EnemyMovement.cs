using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 3f;
    public int damageAmount = 1;

    private void Update()
    {
        MoveTowardsPlayer();
    } 

    private void MoveTowardsPlayer(){ // Move the enemy towards the player
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.position) < 1f) HurtPlayer();
        
    }

    private void HurtPlayer(){
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        player.GetComponent<PlayerHealth>().Hurt(damageAmount, direction);
    }
}