using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Room : MonoBehaviour
{
    public Transform player;
    public RoomCamera cameraReference;
    public float panSpeed = 5f;
    public Transform cameraLocation; //In game manager, move the main camera to this gameobject's location
    
    public Room northRoom, southRoom, eastRoom, westRoom;
    public Transform northSpawn, southSpawn, eastSpawn, westSpawn;
    public List<Enemy> enemies = new List<Enemy>();
    public bool hasComplete = false; // Determines if the room has been beaten
    public bool isOccupied = false; // The player is inside of this room
    public bool isDiscoverable = false; // This room is adjacent to a completed room !! no implementation yet

    public void EnterRoom(string direction){
        if(!hasComplete) return;
        if(direction.Equals("n") && northRoom != null){
            isOccupied = false;
            player.transform.position = northRoom.southSpawn.position;
            cameraReference.target = northRoom.cameraLocation;
            northRoom.SpawnEnemies();
        } else if(direction.Equals("s") && southRoom != null){
            isOccupied = false;
            player.transform.position = southRoom.northSpawn.position;
            cameraReference.target = southRoom.cameraLocation;
            southRoom.SpawnEnemies();
        } else if(direction.Equals("e")  && eastRoom != null){
            isOccupied = false;
            player.transform.position = eastRoom.westSpawn.position;
            cameraReference.target = eastRoom.cameraLocation;
            eastRoom.SpawnEnemies();
        } else if(direction.Equals("w")  && westRoom != null){
            isOccupied = false;
            player.transform.position = westRoom.eastSpawn.position;
            cameraReference.target = westRoom.cameraLocation;
            westRoom.SpawnEnemies();
        }
    }
    public void SpawnEnemies(){
        isOccupied = true;
        if(hasComplete) return;
        foreach(Enemy e in enemies){
            Vector2 pos = e.transform.position;
            e.currentNode = AStarManager.instance.InitializeClosestNode(pos);
            e.player = player;
            Debug.Log("enemy spawned");
            e.Spawn();
        }
    }
    public int GetEnemiesLeft() { //gets # of enemies left besides turrets
        int turretCount = 0;
        foreach(Enemy e in enemies){
            if (e is Turret) turretCount++;
        }
        return enemies.Count - turretCount;
    }
    public void HandleEnemyDeath(EnemyHealth enemyHealth, Enemy enemy){ 
        Debug.Log("Enemy Killed");
        RemoveEnemy(enemy);
    }
    public void RemoveEnemy(Enemy e){
        if(enemies.Contains(e)) enemies.Remove(e);
        Debug.Log("Enemies Left: "+ GetEnemiesLeft());
        if(GetEnemiesLeft() <= 0){
            hasComplete = true;
            Debug.Log("Room Cleared");
            foreach(Enemy n in enemies){
                n.Despawn();
            }
        } 
    }
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        if(GetEnemiesLeft() == 0) hasComplete = true;
    }
    void OnEnable() {
        EnemyHealth.OnEnemyDeath += HandleEnemyDeath; // Subscribe to event
    }

    void OnDisable() {
        EnemyHealth.OnEnemyDeath -= HandleEnemyDeath; // Unsubscribe 
    }
    
}