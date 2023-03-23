using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using TMPro;

public class SummaryManager : MonoBehaviour
{
    public string username;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI timeTakenText;
    public TextMeshProUGUI weakestLearningObjText;
    [SerializeField] GameObject newAchievement;
    [SerializeField] GameObject Star1;
    [SerializeField] GameObject Star2;
    [SerializeField] GameObject Star3;

    private GameRecord result;

    void Start()
    {
        // var url = HttpManager.http_url + "get_gamerecord_user?username=" + DataManager.username;
        var url = HttpManager.http_url + "get_gamerecord_user?username=" + "spaceman";
        List<GameRecord> response = HttpManager.Get<List<GameRecord>>(url);
        result = response[0];
        displaySummary();

        if (result.completed == true)
        {
            postToLeaderboard();
        }
    }

    public void displaySummary()
    {
        getNumberStars(result.numberCorrect);
        scoreText.text = result.score.ToString();
        double accuracy = Math.Round((double)result.numberCorrect / 12, 2) * 100;
        accuracyText.text = string.Format("{0}/12 ({1}%)", result.numberCorrect.ToString(), accuracy.ToString());
        timeTakenText.text = result.timeTaken.ToString() + " mins";
        weakestLearningObjText.text = result.weakestLearningObj;
    }

    public void getNumberStars(int numberCorrect)
    {
        if (numberCorrect <= 5)
        {
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
        else if (numberCorrect <= 9)
        {
            Star3.SetActive(true);
        }
    }

    public void postToLeaderboard()
    {
        var url = HttpManager.http_url + "add_highscore";
        LeaderboardManager.ScoreList scoreList = new LeaderboardManager.ScoreList(DataManager.username, result.score, DataManager.selectedSubject);
        var response = HttpManager.Post(url, scoreList);
    }

}
