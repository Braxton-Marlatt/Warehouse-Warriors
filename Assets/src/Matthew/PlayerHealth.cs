using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; //Start health, initialized to your current health
    private int health;
    public float invincibilityDuration = 1.5f; 
    private float invincibilityTimer = 0f; //Tracks DeltaTime between getting hurt and invincibilityDuration
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    void Start(){
        health = maxHealth;
        UpdateHealthUI(); //test method
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        UpdateInvincibilityTimer();
        UpdateSpriteTransparency();
    }

    public void Hurt(int damage = 1, Vector2? knockbackDirection = null){
        if (isInvincible || health <=0 || ToggleValue.isToggleOn) return;
        health--;
        SoundEffectManager.Instance.PlaySound("playerhit", SoundEffectManager.Instance.audioSources); //Play hit sound
        if (health <= 0){ //Check for no health
            GameOver();
        }else{ //gives the player breif invinciblility on hit
            PlayerMovement movement = GetComponent<PlayerMovement>();
            if (knockbackDirection.HasValue) movement.Knockback(knockbackDirection.Value);
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
        UpdateHealthUI();
    }

    void UpdateInvincibilityTimer(){
        if (isInvincible){
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f){
                isInvincible = false;
            }
        }
    }
    void UpdateSpriteTransparency(){
        if (isInvincible){
            Color color = spriteRenderer.color;
            color.a = 0.7f; // Set alpha to 70%
            spriteRenderer.color = color;
        } else {
            Color color = spriteRenderer.color;
            color.a = 1f; // Set alpha to 100%
            spriteRenderer.color = color;
        }
    }
    //public library methods. Called on by anything 
    public int GetHealth(){
        return health;
    }
    public void SetHealth(int health){
        if(health <= 0){
            this.health = 0;
            GameOver();
        }
        if(health > maxHealth) health = maxHealth;
        this.health = health;
    }
    public void Heal(int amount =1){
        SetHealth(GetHealth() + amount);    
    }

    //temp methods to test health depletion
    void GameOver(){
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
    void UpdateHealthUI(){
        Debug.Log("Health" + health);
    }

    
}