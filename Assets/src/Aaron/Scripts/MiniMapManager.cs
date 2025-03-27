using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    
    public GameObject PlayerDotPrefab; // Prefab reference
    private GameObject playerDotInstance; // The instance of the player dot
    public GameObject MiniMapNodePrefab;
    public Transform minimapParent;
    private Dictionary<Vector2Int, GameObject> minimapNodes = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentRoomPos = Vector2Int.zero;



    private void OnEnable()
    {
        Room.OnRoomEntered += UpdateMinimap;
    }

    private void OnDisable()
    {
        Room.OnRoomEntered -= UpdateMinimap;
    }

private void UpdateMinimap(Room room, Vector2Int direction)
{
    // Ensure that we add the node when the room is entered, regardless of completion
    Debug.Log("Updating minimap for room: " + room);
    
    // Determine new position for the room node
    Vector2Int newPos = currentRoomPos + direction;
    
    // Only add the node if it hasn't been added already
    if (!minimapNodes.ContainsKey(newPos))
    {
        GameObject node = Instantiate(MiniMapNodePrefab, minimapParent);
        node.transform.localPosition = new Vector3(newPos.x * 40f, newPos.y * 26, 0);  // Increase space if needed
        minimapNodes[newPos] = node;
    }

    currentRoomPos = newPos;  // Update the current room position
    
    
}


    private void Start()
{
    if (!minimapNodes.ContainsKey(Vector2Int.zero)) // Ensure the first room is always added at (0,0)
    {
        AddRoomToMinimap(Vector2Int.zero); // Add the first room (starting room)
    }
    if (playerDotInstance == null)
    {
        playerDotInstance = Instantiate(PlayerDotPrefab, minimapParent); // Create an instance
        playerDotInstance.transform.localPosition = Vector3.zero; // Initial position
    }
}

private void AddRoomToMinimap(Vector2Int position)
{
    if (!minimapNodes.ContainsKey(position))
    {
        GameObject node = Instantiate(MiniMapNodePrefab, minimapParent);
        node.transform.localPosition = new Vector3(position.x * 20, position.y * 20, 0);
        minimapNodes[position] = node;
    }
}

    private void Update()
    {
     // Update PlayerDot's position on the minimap
    if (playerDotInstance != null)
    {
        playerDotInstance.transform.localPosition = new Vector3(currentRoomPos.x * 40f, currentRoomPos.y * 26f, 0);
        playerDotInstance.transform.SetAsLastSibling();// Ensure the player dot is always on top of other nodes
    }   
    }

}
