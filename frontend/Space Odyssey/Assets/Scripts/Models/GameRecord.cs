using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class GameRecord
{
    public GameRecord(string username, int score, int numberCorrect, string questionSubject, int questionTopic, string weakestLearningObj, string dateOfGame, bool completed, double timeTaken)
    {
        // this.gameId = gameId;
        this.username = username;
        this.score = score;
        this.numberCorrect = numberCorrect;
        this.questionSubject = questionSubject;
        this.questionTopic = questionTopic;
        this.weakestLearningObj = weakestLearningObj;
        this.dateOfGame = dateOfGame;
        this.completed = completed;
        this.timeTaken = timeTaken;
    }

    // public int gameId { get; set; }
    public string username { get; set; }
    public int score { get; set; }
    public int numberCorrect { get; set; }
    public string questionSubject { get; set; }
    public int questionTopic { get; set; }
    public string weakestLearningObj { get; set; }
    public string dateOfGame { get; set; }
    public bool completed { get; set; }
    public double timeTaken { get; set; }
}