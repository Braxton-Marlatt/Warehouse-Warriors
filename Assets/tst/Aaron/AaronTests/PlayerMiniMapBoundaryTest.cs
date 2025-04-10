using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MinimapBoundary
{
    private GameObject minimapGO;
    private MinimapManager minimapManager;
    private GameObject playerDotPrefab;
    private GameObject miniMapNodePrefab;
    private Transform minimapParent;

    [SetUp]
    public void Setup()
    {
        // Setup base GameObjects
        minimapGO = new GameObject("MinimapManager");
        minimapManager = minimapGO.AddComponent<MinimapManager>();

        minimapParent = new GameObject("MinimapParent").transform;

        // Create a simple prefab for the dot and minimap node
        playerDotPrefab = GameObject.CreatePrimitive(PrimitiveType.Quad);
        playerDotPrefab.name = "PlayerDotPrefab";
        miniMapNodePrefab = new GameObject("MiniMapNode");
        miniMapNodePrefab.AddComponent<MinimapNode>();

        // Assign to the manager
        minimapManager.PlayerDotPrefab = playerDotPrefab;
        minimapManager.MiniMapNodePrefab = miniMapNodePrefab;
        minimapManager.minimapParent = minimapParent;
    }

    [UnityTest]
    public IEnumerator Minimap_PlayerDot_MovesWithRoomEntry()
    {
        // Call Start manually since Unity doesn't do it in tests
        minimapManager.SendMessage("Start");

        // Simulate entering a room to the right (1, 0)
        Vector2Int moveDirection = new Vector2Int(1, 0);
        Room fakeRoom = new GameObject("FakeRoom").AddComponent<Room>();

        typeof(MinimapManager)
            .GetMethod("UpdateMinimap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(minimapManager, new object[] { fakeRoom, moveDirection });

        yield return null;

        // Find the actual player dot object
        GameObject playerDot = null;
        foreach (Transform child in minimapParent)
        {
            if (child.name.Contains("Quad") || child.name.Contains("PlayerDot"))
            {
                playerDot = child.gameObject;
                break;
            }
        }

        Assert.NotNull(playerDot, "Player dot was not created.");
        Assert.AreEqual(new Vector3(40f, 0f, 0f), playerDot.transform.localPosition, "Player dot position incorrect after moving right.");
    }
}
