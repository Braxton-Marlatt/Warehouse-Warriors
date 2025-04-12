using UnityEngine;
using System.Collections.Generic;

public class Boss : Enemy
{
    [Header("Attack Management")]
    public float circularAttackCooldown = 4f;
    public float homingAttackCooldown = 6f;
    private float nextCircularAttackTime;
    private float nextHomingAttackTime;

    [Header("Component References")]
    public BossCircularAttack circularAttack;
    public BossHomingAttack homingAttack;

    [Header("Movement Settings")]
    public float moveSpeedBossBoss = 3f;
    public float waypointThreshold = 0.1f;
    private List<Vector2> waypoints;
    private Vector2 targetPosition;
    private int currentWaypointIndex = -1;
    private Rigidbody2D rb;
    private Vector2 moveSpeedBoss;

    protected void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        // Initialize waypoints
        waypoints = new List<Vector2>
        {
            new Vector2(1f, 2f),
            new Vector2(-4f, 2f),
            new Vector2(-4f, -2f),
            new Vector2(1f, -2f)
        };

        // Set initial target
        SelectNewWaypoint();
        nextCircularAttackTime = Time.time + circularAttackCooldown;
        nextHomingAttackTime = Time.time + homingAttackCooldown;
    }

    protected override void Update()
    {
        base.Update();
        if (!isSpawned) return;

        HandleCircularAttack();
        HandleHomingAttack();
    }

    protected void Movement()
    {
        if (waypoints.Count == 0) return;

        // Check if reached current waypoint
        if (Vector2.Distance(transform.position, targetPosition) <= waypointThreshold)
        {
            SelectNewWaypoint();
        }

        // Move towards target
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * moveSpeedBoss;
    }

    private void SelectNewWaypoint()
    {
        if (waypoints.Count < 2) return;

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (i != currentWaypointIndex)
            {
                availableIndices.Add(i);
            }
        }

        // Select random new waypoint from available indices
        currentWaypointIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        targetPosition = waypoints[currentWaypointIndex];
    }

    private void HandleCircularAttack()
    {
        if (Time.time > nextCircularAttackTime && circularAttack != null)
        {
            circularAttack.ShootCircular();
            nextCircularAttackTime = Time.time + circularAttackCooldown;
        }
    }

    private void HandleHomingAttack()
    {
        if (Time.time > nextHomingAttackTime && homingAttack != null)
        {
            homingAttack.ShootHomingBullet();
            nextHomingAttackTime = Time.time + homingAttackCooldown;
        }
    }
}