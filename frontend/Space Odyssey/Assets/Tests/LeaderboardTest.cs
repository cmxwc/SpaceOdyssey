using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderboardTest
{
    // Leaderboard Tests 
    [UnityTest]
    public IEnumerator check_all_rows_have_name_rank_score()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);

        TMP_Text[] rankOneRankNameScore = GameObject.Find("Row1").GetComponentsInChildren<TMP_Text>();
        Assert.AreEqual(rankOneRankNameScore[0].text, "1");
        Assert.IsFalse(string.IsNullOrEmpty(rankOneRankNameScore[1].text), "The name is empty");
        Assert.IsFalse(string.IsNullOrEmpty(rankOneRankNameScore[2].text), "The name is empty");

        TMP_Text[] rankTwoRankNameScore = GameObject.Find("Row2").GetComponentsInChildren<TMP_Text>();
        Assert.AreEqual(rankTwoRankNameScore[0].text, "2");
        Assert.IsFalse(string.IsNullOrEmpty(rankTwoRankNameScore[1].text), "The name is empty");
        Assert.IsFalse(string.IsNullOrEmpty(rankTwoRankNameScore[2].text), "The name is empty");
    }

    [UnityTest]
    //Check that students are arranged by descending score
    public IEnumerator check_scores_descending()
    {
        SceneManager.LoadScene("LeaderboardScene");
        yield return new WaitForSeconds(3);
        // Check page one

        TMP_Text[] rankOneRankNameScore = GameObject.Find("Row1").GetComponentsInChildren<TMP_Text>();
        TMP_Text[] rankTwoRankNameScore = GameObject.Find("Row2").GetComponentsInChildren<TMP_Text>();
        // RowUI rankTwo = GameObject.Find("Row2").GetComponent<RowUI>();
        // RowUI rankThree = GameObject.Find("Row3").GetComponent<RowUI>();
        // RowUI rankFour = GameObject.Find("Row4").GetComponent<RowUI>();
        // RowUI rankFive = GameObject.Find("Row5").GetComponent<RowUI>();

        int rankOneScore = int.Parse(rankOneRankNameScore[2].text);
        int rankTwoScore = int.Parse(rankTwoRankNameScore[2].text);
        // int rankTwoScore = int.Parse(rankTwo.score.text);
        // int rankThreeScore = int.Parse(rankThree.score.text);
        // int rankFourScore = int.Parse(rankFour.score.text);
        // int rankFiveScore = int.Parse(rankFive.score.text);

        Assert.IsTrue(rankOneScore >= rankTwoScore);
        // Assert.IsTrue(rankTwoScore >= rankThreeScore); 
        // Assert.IsTrue(rankThreeScore >= rankFourScore);
        // Assert.IsTrue(rankFourScore >= rankFiveScore);
    }




}
