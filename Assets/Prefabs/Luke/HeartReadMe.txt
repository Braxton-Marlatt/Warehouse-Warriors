💖 Heart Prefab 💖: The heartPrefab is a collectible item used to reward the player upon an enemy's death. It serves as a health pickup and is instantiated during the loot drop process managed by the EnemyDeathHandler.

********************************************************************

This prefab includes:

	Sprite Renderer: Displays a pixel-art heart.

	Box Collider (2D): Used to detect player collisions for pickup.

	HeartPickup Script: Handles logic for increasing the player's health when collected


********************************************************************
********************************************************************
1. Assigning to Loot System

The prefab is used within a loot drop system defined as follows:
You can assign the heartPrefab to the pickupPrefab field within a LootDrop instance, and configure its dropProbability.

********************************************************************
********************************************************************
2. Integration with Enemy Death System

The prefab is instantiated when an enemy dies. This is handled through the EnemyDeathHandler, which subscribes to an OnEnemyDeath event from the EnemyHealth.

********************************************************************
********************************************************************

3. Assigning in the Scene

Attach the EnemyDeathHandler script to your GameManager or any central game controller object in your scene. Make sure to populate the lootDrops list in the inspector, assigning heartPrefab and adjusting the drop probability as desired.

********************************************************************
********************************************************************

Notes:

	Ensure the HeartPickup script references and modifies the player's health component correctly.

	You can adjust the drop frequency by modifying the dropProbability in the Inspector.

	Add sound effects, particles, or animations to the heartPrefab for better feedback upon collection.

********************************************************************
********************************************************************