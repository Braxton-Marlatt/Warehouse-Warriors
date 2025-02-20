using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public Transform player;
    public Transform cameraReference;
    public float panSpeed = 5f;

    //Doors & Rooms: if either is left unassigned, he player cannot travel in that direction
    //This class only detects if they went through a door. room links / transitions are handled in the game manager
    public Transform northDoor,southDoor,eastDoor,westDoor;
    public Room northRoom, southRoom, eastRoom, westRoom;
    public Transform northSpawn, southSpawn, eastSpawn, westSpawn;

    public Transform cameraLocation; //In game manager, move the main camera to this gameobject's location

    public List<Enemy> enemies = new List<Enemy>();
    private bool hasComplete; // Determines if the room has been beaten

    public void EnterRoom(){
        if(hasComplete) return;
        foreach(Enemy e in enemies){
            e.Spawn();
        }
    }

    public int GetEnemiesLeft(){
        return enemies.Count;
    }

    public void OnEnemyDeath(EnemyHealth enemyHealth, Enemy enemy){
        RemoveEnemy(enemy);
    }

    public void RemoveEnemy(Enemy e){
        if(enemies.Contains(e)){
            enemies.Remove(e);
        }
        if(GetEnemiesLeft() == 0){
            hasComplete = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            if (northDoor != null && northRoom != null && IsTouching(other.transform, northDoor)){
                TransitionToRoom(northRoom,"n");
            }else if (southDoor != null && southRoom != null && IsTouching(other.transform, southDoor)){
                TransitionToRoom(southRoom,"s");
            }else if (eastDoor != null && eastRoom != null && IsTouching(other.transform, eastDoor)){
                TransitionToRoom(eastRoom,"e");
            }else if (westDoor != null && westRoom != null && IsTouching(other.transform, westDoor)){
                TransitionToRoom(westRoom, "w");
            }
        }
    }
    private bool IsTouching(Transform player, Transform door){
        return Vector2.Distance(player.position, door.position) < 0.5f;
    }

    public void TransitionToRoom(Room room, string direction){
        if(direction.Equals("n")){
            player.transform.position = room.southSpawn.position;
            cameraReference.position = room.cameraLocation.position;
        } else if(direction.Equals("s")){
            player.transform.position = room.northSpawn.position;
            cameraReference.position = room.cameraLocation.position;
        } else if(direction.Equals("e")){
            player.transform.position = room.westSpawn.position;
            cameraReference.position = room.cameraLocation.position;
        } else if(direction.Equals("w")){
            player.transform.position = room.eastSpawn.position;
            cameraReference.position = room.cameraLocation.position;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GetEnemiesLeft() == 0){
            hasComplete = true;
        }
    }
}
