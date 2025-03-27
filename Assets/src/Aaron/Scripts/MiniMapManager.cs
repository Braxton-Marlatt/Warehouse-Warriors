using System.Collections.Generic;
using UnityEngine;

// SINGLETON IMPLEMENTATION IN MINIMAPMANAGER
// Ensures only one instance of MinimapManager exists at any time
public class MinimapManager : MonoBehaviour
{
    // The private static instance of the MinimapManager class
    // This property is part of the Singleton pattern: it ensures there is only one instance of MinimapManager
    // and provides global access to that instance. The `private set` ensures that the instance can only
    // be set within the MinimapManager class, maintaining the Singleton's guarantee of a single instance.

    public static MinimapManager Instance { get; private set; }

    public GameObject PlayerDotPrefab; // Prefab reference for player dot
    private GameObject playerDotInstance; // The actual player dot instance
    public GameObject MiniMapNodePrefab; // Reference to the room node prefab
    public Transform minimapParent; // Parent object for minimap elements
    private Dictionary<Vector2Int, GameObject> minimapNodes = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int currentRoomPos = Vector2Int.zero;

    private void Awake()
    {
        // Ensure only one instance exists (SINGLETON)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps the manager between scenes if needed
    }

    private void OnEnable()
    {
        Room.OnRoomEntered += UpdateMinimap;
    }

    private void OnDisable()
    {
        Room.OnRoomEntered -= UpdateMinimap;
    }

    private void Start()
    {
        // Ensure the first room node is added at (0,0)
        if (!minimapNodes.ContainsKey(Vector2Int.zero))
        {
            AddRoomToMinimap(Vector2Int.zero);
        }

        // Create player dot instance (if not already created)
        if (playerDotInstance == null)
        {
            playerDotInstance = Instantiate(PlayerDotPrefab, minimapParent);
            playerDotInstance.transform.localPosition = Vector3.zero;
        }
    }

    private void UpdateMinimap(Room room, Vector2Int direction)
    {
        Vector2Int newPos = currentRoomPos + direction;

        // Add a new room node if it doesn't already exist
        if (!minimapNodes.ContainsKey(newPos))
        {
            AddRoomToMinimap(newPos);
        }

        currentRoomPos = newPos;
    }

private void AddRoomToMinimap(Vector2Int position)
{
    if (!minimapNodes.ContainsKey(position))
    {
        if (MiniMapNodePrefab == null)
        {
            Debug.LogError("MiniMapNodePrefab is not assigned in the Inspector!");
            return;
        }
        // PROTOTYPE DESIGN PATTERN
        // The MinimapNodePrototype component on the MiniMapNodePrefab acts as the Prototype.
        // The Clone method creates a new instance of the node by copying the existing prototype, 
        // allowing us to easily create multiple copies of the room node without manually instantiating them from scratch.
        MinimapNodePrototype prototype = MiniMapNodePrefab.GetComponent<MinimapNodePrototype>();
        if (prototype == null)
        {
            Debug.LogError("MiniMapNodePrefab does not have a MinimapNodePrototype component!");
            return;
        }

        Vector3 nodePosition = new Vector3(position.x * 40f, position.y * 26f, 0);
        MinimapNodePrototype clonedNode = prototype.Clone(nodePosition, minimapParent);
        minimapNodes[position] = clonedNode.gameObject;
    }
}


    private void Update()
    {
        // Update player dot's position based on current room position
        if (playerDotInstance != null)
        {
            playerDotInstance.transform.localPosition = new Vector3(currentRoomPos.x * 40f, currentRoomPos.y * 26f, 0);
            playerDotInstance.transform.SetAsLastSibling(); // Keep player dot on top of other nodes
        }
    }
}
