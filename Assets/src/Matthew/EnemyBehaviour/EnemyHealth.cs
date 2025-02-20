using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public static event Action<EnemyHealth, Enemy> OnEnemyDeath;

    public int health = 5;
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
        Enemy enemy = GetComponent<Enemy>();
        OnEnemyDeath?.Invoke(this,enemy);
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
