using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; //Start health, initialized to your current health
    private int health;
    public float invincibilityDuration = 2f; 
    private float invincibilityTimer = 0f; //Tracks DeltaTime between getting hurt and invincibilityDuration
    private bool isInvincible = false;

    void Start(){
        health = maxHealth;
        UpdateHealthUI(); //test method
    }

    // Update is called once per frame
    void Update(){
        UpdateInvincibilityTimer();
    }

    public void Hurt(int damage = 1, Vector2? knockbackDirection = null){
        if (isInvincible || health <=0) return;
        health--;

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

    //public library methods. Called on by anything 
    public int GetHealth(){
        return health;
    }
    public void SetHealth(int health){
        if(health <= 0){
            this.health = 0;
            GameOver();
        }
        this.health = health;
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