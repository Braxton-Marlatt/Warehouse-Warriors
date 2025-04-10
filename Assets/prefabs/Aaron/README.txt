# Minimap System Prefab

A simple and modular minimap system for 2D games in Unity.

This prefab system provides a lightweight, room-based minimap that dynamically updates as the player explores new areas. It uses the Singleton and Prototype design patterns for efficient management and instantiation of minimap elements.

---

## Prefabs Included

### MinimapManager
Handles all minimap logic and controls the creation and positioning of minimap elements.

#### Features:
- Singleton Pattern — Ensures only one instance exists.
- Listens for room transitions via `Room.OnRoomEntered`.
- Tracks player position on the minimap.
- Automatically creates new room nodes.

#### Required References:
| Field              | Description                           |
|-------------------|---------------------------------------|
| PlayerDotPrefab    | Green dot representing the player.    |
| MiniMapNodePrefab  | Red box representing a discovered room. |
| MinimapParent      | UI container for minimap elements.    |

---

### MinimapNodePrefab
Represents a discovered room on the minimap.

#### Features:
- Red box visual.
- Prototype Pattern — Instantiates new nodes by cloning the prototype.
- Automatically added when entering a new room.

---

### PlayerDotPrefab
Visual indicator of the player’s current position on the minimap.

#### Features:
- Green dot visual.
- Updates position based on current room.

---

### MinimapParent
UI container for all minimap elements.

#### Features:
- Semi-transparent gray background.
- Organizes minimap nodes and player dot.

---

## Setup Instructions

1. Drag the `MinimapManager` prefab into your scene.
2. Assign the following in the Inspector:
   - `PlayerDotPrefab`
   - `MiniMapNodePrefab`
   - `MinimapParent`
3. Ensure your room system triggers `Room.OnRoomEntered` when the player enters a new room.
4. Play the scene:
   - The starting room is automatically added.
   - New rooms are added when explored.
   - The player dot updates its position dynamically.

---

## Requirements

- Unity 2022.3.42f1 or later

---

## Notes

This system is designed to be lightweight and customizable.  
Feel free to:
- Swap out the room and player visuals with your own sprites.
- Adjust node spacing by modifying `AddRoomToMinimap()` in `MinimapManager`.
- Extend `MinimapNode` for additional behavior (e.g., icons, labels).

---

