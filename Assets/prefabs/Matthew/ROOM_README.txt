Using the Room prefabs:

Contains:
- Room object
- List of enemies in the Room
- Sprites for walls / floor
- Doors
- Spawn locations (where the player teleports when player uses a door)
- Camera location (where the camera goes when player uses a door)

How to Create a Room:
1. Copy Room1 for consistent door, spawn, wall, and floor placements
2. Drag and drop enemy/spike prefabs into the room prefab. They will be auto assigned to nodes on start
3. In the room object, drag and drop all enemies to the enemies list (do not include spikes)

How to Add a Room to the Game:
1. Find / create the RoomManager object on the scene
2. In the public room list, add your room prefab and alter the number of rooms generated if desired
3. Rooms are generated and linked together at random when starting the scene.