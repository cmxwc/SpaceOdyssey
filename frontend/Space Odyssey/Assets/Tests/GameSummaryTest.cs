using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSummaryTest
{
    // Game Summary Tests 
    [UnityTest]
    public IEnumerator summary_ui_components_present()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("SummaryScene");
        yield return new WaitForSeconds(2);

        GameObject mainScore = GameObject.Find("Main Score");
        GameObject additionalStats = GameObject.Find("Additional Stats");

        Assert.IsNotNull(mainScore, "No main score found");
        Assert.IsNotNull(additionalStats, "No additional stats found");
    }


}
