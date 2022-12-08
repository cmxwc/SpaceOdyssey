using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static User;


[System.Serializable]
public class Student : User
{
    public Student(string username, int classNumber, int highestScore, int numOfGamesCompleted, List<int> levelsUnlocked, string lastLoginDay)
    {
        this.username = username;
        this.classNumber = classNumber;
        this.highestScore = highestScore;
        this.numOfGamesCompleted = numOfGamesCompleted;
        this.levelsUnlocked = levelsUnlocked;
        this.lastLoginDay = lastLoginDay;
    }
    public int classNumber { get; set; }
    public int highestScore { get; set; } // for leaderboard
    public int numOfGamesCompleted { get; set; }
    public List<int> levelsUnlocked { get; set; }
    public string lastLoginDay;
}