using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class MinimapNodeCreationTest
{
    private MinimapManager minimapManager;
    private Transform minimapParent;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject("MinimapManager");
        minimapManager = go.AddComponent<MinimapManager>();

        minimapParent = new GameObject("MinimapParent").transform;

        // Set required prefabs
        GameObject dot = new GameObject("Dot");
        minimapManager.PlayerDotPrefab = dot;

        GameObject nodePrefab = new GameObject("Node");
        nodePrefab.AddComponent<MinimapNode>();
        minimapManager.MiniMapNodePrefab = nodePrefab;

        minimapManager.minimapParent = minimapParent;
    }

    [UnityTest]
    public IEnumerator Minimap_AddsNewRoomNode_OnRoomEntry()
    {
        // Manually invoke Start
        minimapManager.SendMessage("Start");

        // Enter a new room at (0, 1)
        Vector2Int moveDirection = new Vector2Int(0, 1);
        Room mockRoom = new GameObject("MockRoom").AddComponent<Room>();

        typeof(MinimapManager)
            .GetMethod("UpdateMinimap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(minimapManager, new object[] { mockRoom, moveDirection });

        yield return null;

        // Look for a node at expected position
        Vector3 expectedPosition = new Vector3(0f, 26f, 0f);
        bool nodeFound = false;

        foreach (Transform child in minimapParent)
        {
            if (child.localPosition == expectedPosition)
            {
                nodeFound = true;
                break;
            }
        }

        Assert.IsTrue(nodeFound, "No room node was created at the expected position (0, 1).");
    }
}
