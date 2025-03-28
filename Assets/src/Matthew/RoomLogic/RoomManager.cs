using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomManager : MonoBehaviour
{
    /**
     * Generates rooms randomly
     * All rooms are linked
     * Deletes doors/spawn points that are unused
     * Grabs rooms from prefabs
     */

    public Transform player;
    public RoomCamera cameraReference;
    //Keep track of the map:
    public Dictionary<Vector2Int, Room> roomMap = new Dictionary<Vector2Int, Room>();
    public GameObject spawnPrefab; //Spawn Room game object reference
    //prefab references
    public GameObject bossRoomPrefab;
    public GameObject shopRoomPrefab; // Assign your shop room prefab in the Inspector
    public List<GameObject> roomPrefabs = new List<GameObject>(); //include shop prefab
    private List<GameObject> usedRoomPrefabs = new List<GameObject>();
    public int spawnedRooms = 12;
    private int count = 0;
    private bool shopRoomSpawned = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeAllRooms();
    }

    /**
    Logic (on start):
    - You cannot have repeat rooms until all rooms are used
    - select 1 random room w/ < 4 adjacent rooms
    - select a random direction, link a room in that direction
    - repeat until no rooms are left (including shop)
    - find the room furthest from spawn w/ < 4 adjacent rooms
    - pick a random direction, link the boss room to it
    */
    public void InitializeAllRooms(){
        InitializeRoom(0,0,spawnPrefab); //set the spawn room at 0,0
        for(int i = 0; i <= spawnedRooms; i++){
            GameObject prefab = GetRandomRoomPrefab();
            Vector2Int randRoom = GetRandomRoom();
            Vector2Int newRoom = GetRandomDirection(randRoom.x,randRoom.y);
            InitializeRoom(newRoom.x,newRoom.y,prefab);
        }
        DeleteUnusedDoors();
        SetDoorStates();
    }
    private void DeleteUnusedDoors()
    {
        foreach (var kvp in roomMap)
        {
            Room r = kvp.Value;
            Transform doorsParent = r.transform.Find("Doors"); // Find the "Doors" parent object
            if (doorsParent != null)
            {
                if (r.northRoom == null){
                    Transform northDoor = doorsParent.Find("North Door");
                    if (northDoor != null) Destroy(northDoor.gameObject);
                }
                if (r.southRoom == null){
                    Transform southDoor = doorsParent.Find("South Door");
                    if (southDoor != null) Destroy(southDoor.gameObject);
                }
                if (r.eastRoom == null){
                    Transform eastDoor = doorsParent.Find("East Door");
                    if (eastDoor != null) Destroy(eastDoor.gameObject);
                }
                if (r.westRoom == null){
                    Transform westDoor = doorsParent.Find("West Door");
                    if (westDoor != null) Destroy(westDoor.gameObject);
                }
            }
        }
        Debug.Log("Deleted Unused Doors");
    }
    //if a room is completed, open all doors
    //if a door leads to the boss room, use boss room door
    private void SetDoorStates(){

    }
    private GameObject GetRandomRoomPrefab()
    {
        if (!shopRoomSpawned)
        {
            shopRoomSpawned = true;
            return shopRoomPrefab;
        }

        // If all rooms have been used, reset the list
        if (roomPrefabs.Count == 0) {
            roomPrefabs.AddRange(usedRoomPrefabs);
            usedRoomPrefabs.Clear();
        }

        // Pick a random room from the available prefabs
        int index = Random.Range(0, roomPrefabs.Count);
        GameObject selectedPrefab = roomPrefabs[index];

        // Move the selected room to the used list and remove it from available rooms
        roomPrefabs.RemoveAt(index);
        usedRoomPrefabs.Add(selectedPrefab);

        return selectedPrefab; // Just return the prefab without instantiating it
    }

    private Vector2Int GetRandomRoom()
    {
        List<Vector2Int> validRoomPositions = new List<Vector2Int>();
        foreach (var kvp in roomMap){
            Vector2Int position = kvp.Key;
            if (IsValidRoom(position.x, position.y)) {
                validRoomPositions.Add(position);
            }
        }
        if (validRoomPositions.Count == 0) return new Vector2Int(-1, -1);
        return validRoomPositions[Random.Range(0, validRoomPositions.Count)];
    }
    private bool IsValidRoom(int x, int y){ // Checks if the room has 4 connections
        Room northRoom = GetRoom(x, y + 1);
        Room southRoom = GetRoom(x, y - 1);
        Room eastRoom = GetRoom(x + 1, y);
        Room westRoom = GetRoom(x - 1, y);
        if (northRoom == null) return true;
        if (southRoom == null) return true;
        if (eastRoom == null) return true;
        if (westRoom == null) return true;
        return false;
    }
    private Vector2Int GetRandomDirection(int x, int y)
    {
        List<Vector2Int> possibleDirections = new List<Vector2Int>();
        if (GetRoom(x, y + 1) == null) possibleDirections.Add(new Vector2Int(x, y + 1)); // North
        if (GetRoom(x, y - 1) == null) possibleDirections.Add(new Vector2Int(x, y - 1)); // South
        if (GetRoom(x + 1, y) == null) possibleDirections.Add(new Vector2Int(x + 1, y)); // East
        if (GetRoom(x - 1, y) == null) possibleDirections.Add(new Vector2Int(x - 1, y)); // West
        if (possibleDirections.Count == 0) return new Vector2Int(-1, -1);
        return possibleDirections[Random.Range(0, possibleDirections.Count)];
    }

    //places a room on the map and links it to adjacent rooms
    public void InitializeRoom(int x, int y, GameObject roomPrefab){
        Room room = PlaceRoomPrefab(x,y,roomPrefab);
        room.player = player;
        room.cameraReference = cameraReference;
        room.name = ""+count;
        count++;
        AddRoom(x,y,room);
        LinkRoom(x,y);
    }
    private Room PlaceRoomPrefab(int x, int y, GameObject roomPrefab)
    {
        if (roomPrefab == null){
            Debug.LogError("Room prefab is null");
            return null;
        }

        // Room position calculator
        float posX = x * 30f; 
        float posY = y * 20f; 
        Vector3 roomPosition = new Vector3(posX, posY, 0f);

        // Instantiate only here
        GameObject roomInstance = Instantiate(roomPrefab, roomPosition, Quaternion.identity);
        Room room = roomInstance.GetComponent<Room>();

        Debug.Log($"Placed room {roomPrefab.name} at ({x}, {y}) -> World Position: {roomPosition}");
        return room;
    }
    //adds room to the map data structure
    private void AddRoom(int x, int y, Room room)
    {
        Vector2Int position = new Vector2Int(x, y);
        roomMap[position] = room;
    }

    public Room GetRoom(int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        return roomMap.TryGetValue(position, out Room room) ? room : null;
    }
    private void LinkRoom(int x, int y)
    {
        Room currentRoom = GetRoom(x, y);
        if (currentRoom == null) return; // No room at these coordinates

        Room northRoom = GetRoom(x, y + 1);
        Room southRoom = GetRoom(x, y - 1);
        Room eastRoom = GetRoom(x + 1, y);
        Room westRoom = GetRoom(x - 1, y);

        if (northRoom != null){
            currentRoom.northRoom = northRoom;
            northRoom.southRoom = currentRoom;
        }else{
            currentRoom.northRoom = null;
        }
        if (southRoom != null){
            currentRoom.southRoom = southRoom;
            southRoom.northRoom = currentRoom;
        }else{
            currentRoom.southRoom = null;
        }
        if (eastRoom != null){
            currentRoom.eastRoom = eastRoom;
            eastRoom.westRoom = currentRoom;
        }else{
            currentRoom.eastRoom = null;
        }
        if (westRoom != null){
            currentRoom.westRoom = westRoom;
            westRoom.eastRoom = currentRoom;
        }else{
            currentRoom.westRoom = null;
        }
    }
}
