using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class MinimapNoDuplicateNodeTest
{
    private MinimapManager minimapManager;
    private Transform minimapParent;
    private GameObject nodePrefab;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject("MinimapManager");
        minimapManager = go.AddComponent<MinimapManager>();

        minimapParent = new GameObject("MinimapParent").transform;

        minimapManager.PlayerDotPrefab = new GameObject("Dot");

        nodePrefab = new GameObject("Node");
        nodePrefab.AddComponent<MinimapNode>();
        minimapManager.MiniMapNodePrefab = nodePrefab;
        minimapManager.minimapParent = minimapParent;
    }

    [UnityTest]
    public IEnumerator Minimap_DoesNotDuplicateRoomNode()
    {
        minimapManager.SendMessage("Start");

        Vector2Int direction = new Vector2Int(1, 0);
        Room fakeRoom = new GameObject("FakeRoom").AddComponent<Room>();

        // First room entry
        typeof(MinimapManager)
            .GetMethod("UpdateMinimap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(minimapManager, new object[] { fakeRoom, direction });

        yield return null;

        int nodeCountAfterFirst = minimapParent.childCount;

        // Re-enter same room again (same direction = same resulting position)
        typeof(MinimapManager)
            .GetMethod("UpdateMinimap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(minimapManager, new object[] { fakeRoom, Vector2Int.zero }); // no move

        yield return null;

        int nodeCountAfterSecond = minimapParent.childCount;

        Assert.AreEqual(nodeCountAfterFirst, nodeCountAfterSecond,
            "A duplicate node was created at the same position.");
    }
}
