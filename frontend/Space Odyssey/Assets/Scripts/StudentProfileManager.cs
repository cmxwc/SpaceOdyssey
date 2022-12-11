using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using TMPro;

public class StudentProfileManager : MonoBehaviour
{
    private string http_url = "http://localhost:8000/";
    public string username;
    public TextMeshProUGUI usernameLabel;
    public TextMeshProUGUI classLabel;
    public TextMeshProUGUI highestScoreLabel;
    public TextMeshProUGUI numOfGamesCompletedLabel;
    public TextMeshProUGUI subjectsTakenLabel;
    public TextMeshProUGUI lastLoginLabel;
    private HttpManager http;
    void Start()
    {
        username = DataManager.username;
        if (SceneLoaderManager.CurrentScene() == "WelcomeScene")
        {
            displayWelcomeMessage();
        }
        else if (SceneLoaderManager.CurrentScene() == "ProfileScene")
        {
            displayUserData();
        }

    }

    public void displayWelcomeMessage()
    {
        usernameLabel.text = "Welcome back, " + username;
    }

    public void displayUsername()
    {
        usernameLabel.text = username;

    }

    public class StudentProfileDetails
    {
        public StudentProfileDetails(string username, int classNumber, int highestScore, int numOfGamesCompleted, List<int> levelsUnlocked, List<string> subjectsTaken, string lastLoginDay)
        {
            this.username = username;
            this.classNumber = classNumber;
            this.highestScore = highestScore;
            this.numOfGamesCompleted = numOfGamesCompleted;
            this.levelsUnlocked = levelsUnlocked;
            this.subjectsTaken = subjectsTaken;
            this.lastLoginDay = lastLoginDay;
        }

        public string username { get; set; }
        public int classNumber { get; set; }
        public int highestScore { get; set; } // for leaderboard
        public int numOfGamesCompleted { get; set; }
        public List<int> levelsUnlocked { get; set; }
        public List<string> subjectsTaken { get; set; }
        public string lastLoginDay;
    }

    public void displayUserData()
    {
        var url = http_url + "get_userData?username=" + username;
        StudentProfileDetails profileDetails = HttpManager.Get<StudentProfileDetails>(url);  // Must specify the <TResultType> when calling the method
        Debug.Log(profileDetails.classNumber);
        usernameLabel.text = username;
        classLabel.text = profileDetails.classNumber.ToString();
        highestScoreLabel.text = profileDetails.highestScore.ToString();
        numOfGamesCompletedLabel.text = profileDetails.numOfGamesCompleted.ToString();
        string allSubjects = string.Join(", ", profileDetails.subjectsTaken);  // change list to a concatenated string
        subjectsTakenLabel.text = allSubjects;
        lastLoginLabel.text = profileDetails.lastLoginDay;
    }


}
