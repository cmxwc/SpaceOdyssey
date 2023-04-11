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
        PostAfterGame();
        displaySummary();

        if (GameDataManager.completed == true)
        {
            postToLeaderboard();
        }
    }

    public void PostAfterGame()
    {
        DateTime endTime = DateTime.Now;
        GameDataManager.duration = Math.Round((endTime - GameDataManager.startTime).TotalMinutes, 2);
        GameRecord gameRecord = new GameRecord(DataManager.username, GameDataManager.score, GameDataManager.numberCorrectGame, DataManager.selectedSubject, DataManager.selectedTopic, GameDataManager.getWeakestLearningObj(), DateTime.Now.ToString(), GameDataManager.completed, GameDataManager.duration);
        var url = HttpManager.http_url + "add_gamerecord";
        var response = HttpManager.Post(url, gameRecord);
        Debug.Log(response);
    }

    public void displaySummary()
    {
        getNumberStars(GameDataManager.numberCorrectGame);
        scoreText.text = GameDataManager.score.ToString();
        double accuracy = Math.Round((double)GameDataManager.numberCorrectGame / (12 * 3), 2) * 100;
        accuracyText.text = string.Format("{0}/12 ({1}%)", GameDataManager.numberCorrectGame.ToString(), accuracy.ToString());
        timeTakenText.text = GameDataManager.duration.ToString() + " mins";
        weakestLearningObjText.text = GameDataManager.getWeakestLearningObj();
    }

    private void getNumberStars(int numberCorrect)
    {
        if (numberCorrect <= 5 * 3)
        {
            Star2.SetActive(true);
            Star3.SetActive(true);
        }
        else if (numberCorrect <= 9 * 3)
        {
            Star3.SetActive(true);
        }
    }

    public void postToLeaderboard()
    {
        var url = HttpManager.http_url + "add_highscore";
        LeaderboardManager.ScoreList scoreList = new LeaderboardManager.ScoreList(DataManager.username, GameDataManager.score, DataManager.selectedSubject);
        var response = HttpManager.Post(url, scoreList);
    }

}
