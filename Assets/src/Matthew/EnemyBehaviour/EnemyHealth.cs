using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    public void Hurt(int damage=1){
        health -= damage;
        if (health <= 0){
            Die();
        }
        Debug.Log("HIT!");
    }
    public int GetHealth(){
        return health;
    }
    public void SetHealth(int health){
        if(health <= 0){
            this.health = 0;
            Die();
        }
        this.health = health;
    }
    private void Die(){
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
