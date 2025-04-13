// Script to control the boss behavior in the game

using UnityEngine;

public class Boss : Enemy
{
    [Header("Circle Shoot Settings")]
    public BossCircleShooter circleShooter;

    [Header("Homing Shoot Settings")]
    public BossHomingShooter homingShooter;

    [Header("Attack Timing")]
    public float attackInterval = 10f; // Changed to 10 seconds
    private float nextAttackTime;
    private bool useCircleAttack = true; // Alternates between attacks

    protected override void Update()
    {
        base.Update();
        if (!isSpawned) return;

        Attack();
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (useCircleAttack)
            {
                HandleCircleShoot();// Trigger circle shoot
            }
            else
            {
                HandleHomingShoot();// Trigger homing shoot
            }

            useCircleAttack = !useCircleAttack; // Toggle attack type
            nextAttackTime = Time.time + attackInterval;
        }
    }

    void HandleCircleShoot()
    {
        if (circleShooter != null)
        {
            circleShooter.TriggerBurst();
        }
    }

    void HandleHomingShoot()
    {
        if (homingShooter != null)
        {
            homingShooter.ShootHoming();
        }
    }
}