using UnityEngine;
using System;
using System.Collections;
using System.ComponentModel;

public class EnemyHealth : MonoBehaviour
{
    public static event Action<EnemyHealth, Enemy> OnEnemyDeath;
 
    public int health = 5;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }
    public void Hurt(int damage=1){
        health -= damage;
        SoundFXManager.Instance.PlaySound("EnemyHit"); // Play enemy hit sound
        if (health <= 0){
            Die();
        }else StartCoroutine(FlashRed());
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
        SoundFXManager.Instance.PlaySound("EnemyDeath"); // Play enemy death sound
        SoundFXManager.Instance.StopSoundEffect("ShoppingCart"); // Stop ShoppingCart sound
        OnEnemyDeath?.Invoke(this,enemy);
        Destroy(gameObject); // Destroy the enemy GameObject
    }
    private IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            Debug.Log("Flashing Red");
            spriteRenderer.color = Color.red; 
            yield return new WaitForSeconds(flashDuration);
            Debug.Log("Reverting Color");
            spriteRenderer.color = originalColor;
        }
    }
} 