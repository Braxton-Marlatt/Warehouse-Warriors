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

    //Keep track of the map:
    public Dictionary<Vector2Int, Room> roomMap = new Dictionary<Vector2Int, Room>();


    //Spawn Room game object reference
    public GameObject spawn;
    //prefab reference
    public GameObject shopPrefab;
    public GameObject bossRoomPrefab;
    public List<GameObject> roomPrefabs = new List<GameObject>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
    Logic (on start):
    - You cannot have repeat rooms
    - From spawn, pick 2 random directions
    - pick 2 random rooms
    - select 1 random room w/ < 4 adjacent rooms
    - select a random direction, link a room in that direction
    - repeat until no rooms are left (including shop)
    - find the room furthest from spawn w/ < 4 adjacent rooms
    - pick a random direction, link the boss room to it
    */

    //places a room on the map and links it to adjacent rooms
    public void InitializeRoom(int x, int y, GameObject roomPrefab){
        PlaceRoomPrefab(roomPrefab);
        Room room = GetRoomFromPrefab(roomPrefab);
        AddRoom(x,y,room);
        LinkRoom(x,y);
    }
    private void PlaceRoomPrefab(GameObject roomPrefab){

    }
    //Extract the room object from a room prefab
    public Room GetRoomFromPrefab(GameObject roomPrefab){
        if (roomPrefab == null){
            Debug.LogError("Room prefab is null!");
            return null;
        }
        Room room = roomPrefab.GetComponent<Room>();
        if (room == null){
            Debug.LogError("Room component not found on the prefab: " + roomPrefab.name);
        }
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

    private void LinkRoom(int x, int y){

    }
}
