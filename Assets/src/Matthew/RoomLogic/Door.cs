using UnityEngine;

public class Door : MonoBehaviour
{
    //Doors & Rooms: if either is left unassigned, he player cannot travel in that direction
    //This class only detects if they went through a door. room links / transitions are handled in the game manager
     public Room room;
     public Direction direction;
     public enum Direction{
        North,
        South,
        East,
        West,
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (!other.CompareTag("Player")) return;
        switch (direction){
            case Direction.North:
                room.EnterRoom("n");
                return;
            case Direction.South:
                room.EnterRoom("s");
                return;
            case Direction.East:
                room.EnterRoom("e");
                return;
            case Direction.West:
                room.EnterRoom("w");
                return;
        }
    }
}
