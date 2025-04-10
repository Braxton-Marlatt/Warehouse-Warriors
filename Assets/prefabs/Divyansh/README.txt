Prefab Name: Player
 
Purpose:
This prefab represents the player character in the game. It includes movement, shooting mechanics, animation transitions (blend tree and shooting animation), and knockback logic.
 
Components:
- Rigidbody2D: Handles the physics movement of the player.
- Animator: Manages animation states including Walk, Idle, and Shoot using a blend tree and trigger.
- SpriteRenderer: Displays the player's sprite and animations.
- PlayerShooter.cs: Controls shooting, sets the "Shoot" trigger to play the shooting animation.
- PlayerMovement.cs: Controls walking and idle animation using blend tree.
- Collider2D: Manages collisions with walls, enemies, and environmental elements.
 
Questions Answered:
- How does the player shoot and animate at the same time?
  -> The shooting logic is handled in PlayerShooter.cs and triggers the shooting animation.
- How are transitions between idle and walk handled?
  -> A blend tree is used based on player velocity to handle movement animations.
- What prevents the player from moving off-screen?
  -> Boundary logic in test cases ensures the player remains within visible areas.
 
Who would use this:
- Teammates working on animation or combat systems.
- Testers validating player behavior.
- Developers extending the player functionality.
 
Possible follow-up questions:
- How to replace or extend the shooting animation?
- How to modify movement speed or animation blending?
- How to add a new mechanic without affecting the current structure?