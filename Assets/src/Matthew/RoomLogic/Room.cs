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

    public void EnterRoom(string direction){
        if(!hasComplete) return;
        if(direction.Equals("n") && northRoom != null){
            player.transform.position = northRoom.southSpawn.position;
            cameraReference.target = northRoom.cameraLocation;
            northRoom.SpawnEnemies();
        } else if(direction.Equals("s") && southRoom != null){
            player.transform.position = southRoom.northSpawn.position;
            cameraReference.target = southRoom.cameraLocation;
            southRoom.SpawnEnemies();
        } else if(direction.Equals("e")  && eastRoom != null){
            player.transform.position = eastRoom.westSpawn.position;
            cameraReference.target = eastRoom.cameraLocation;
            eastRoom.SpawnEnemies();
        } else if(direction.Equals("w")  && westRoom != null){
            player.transform.position = westRoom.eastSpawn.position;
            cameraReference.target = westRoom.cameraLocation;
            westRoom.SpawnEnemies();
        }
    }
    public void SpawnEnemies(){
        if(hasComplete) return;
        foreach(Enemy e in enemies){
            Debug.Log("enemy spawned");
            e.Spawn();
        }
    }
    public int GetEnemiesLeft() { return enemies.Count; }
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