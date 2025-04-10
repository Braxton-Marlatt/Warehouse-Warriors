using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class MinimapSingletonTest
{
    [UnityTest]
    public IEnumerator MinimapManager_OnlyOneInstanceExists()
    {
        // Create the first MinimapManager
        GameObject firstGO = new GameObject("MinimapManager1");
        MinimapManager firstInstance = firstGO.AddComponent<MinimapManager>();
        firstInstance.PlayerDotPrefab = new GameObject("Dot");
        GameObject nodePrefab = new GameObject("Node");
        nodePrefab.AddComponent<MinimapNode>(); // MinimapNode inherits from MinimapNodePrototype
        firstInstance.MiniMapNodePrefab = nodePrefab;

        firstInstance.minimapParent = new GameObject("Parent").transform;

        yield return null;

        // Create a second MinimapManager which should destroy itself
        GameObject secondGO = new GameObject("MinimapManager2");
        MinimapManager secondInstance = secondGO.AddComponent<MinimapManager>();

        yield return null;

        // Assert Singleton instance is the first one
        Assert.AreSame(firstInstance, MinimapManager.Instance, "Singleton instance was not preserved.");

        // Assert second instance destroyed itself
        Assert.IsTrue(secondInstance == null || secondInstance.gameObject == null || secondInstance.gameObject.Equals(null),
            "Second instance of MinimapManager was not destroyed as expected.");
    }
}
