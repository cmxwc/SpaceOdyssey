using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using System;
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
    public void displayUserData()
    {
        var url = http_url + "get_userData?username=" + username;
        Student profileDetails = HttpManager.Get<Student>(url);  // Must specify the <TResultType> when calling the method
        usernameLabel.text = username;
        classLabel.text = profileDetails.classNumber.ToString();
        highestScoreLabel.text = profileDetails.highestScore.ToString();
        numOfGamesCompletedLabel.text = profileDetails.numOfGamesCompleted.ToString();
        string allSubjects = string.Join(", ", profileDetails.subjectsTaken);  // change list to a concatenated string
        subjectsTakenLabel.text = allSubjects;
        lastLoginLabel.text = profileDetails.lastLoginDay;
    }

    // Student Last Login Details Class
    public class StudentLastLoginDetails
    {
        public StudentLastLoginDetails(string username, string lastLoginDay)
        {
            this.username = username;
            this.lastLoginDay = lastLoginDay;
        }

        public string username { get; set; }
        public string lastLoginDay { get; set; }

    }
    public void logOut()
    {
        var url = HttpManager.http_url + "update_userData_login";
        StudentLastLoginDetails newLastLoginDetails = new StudentLastLoginDetails(username, DateTime.Now.ToString());
        var response = HttpManager.Post(url, newLastLoginDetails);
        Debug.Log("post" + response);
        SceneLoaderManager.LoadMainPageScene();
    }


}
