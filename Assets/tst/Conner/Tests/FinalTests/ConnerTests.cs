using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class HelpNavigationTests
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        SceneManager.LoadScene("Start_Menu");
        yield return new WaitForSecondsRealtime(0.5f);
    }

    [UnityTest]
    public IEnumerator HelpFromHomeReturnsToHome()
    {
        HelpMenuTracker.source = "start";
        SceneManager.LoadScene("helpMenu");
        yield return new WaitForSecondsRealtime(0.5f);

        // Simulate pressing "Return" from Help
        var manager = GameObject.FindFirstObjectByType<helpMenuManager>();
        Assert.IsNotNull(manager, "HelpMenuManager not found in helpMenu scene");
        manager.returnFromHelp();

        yield return new WaitForSecondsRealtime(0.5f);

        Assert.AreEqual("Start_Menu", SceneManager.GetActiveScene().name, "Did not return to Start_Menu from Help");
    }
}
