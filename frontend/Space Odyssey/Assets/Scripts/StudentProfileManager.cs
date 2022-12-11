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

    public void logOut()
    {
        var url = http_url + "get_userData?username=" + username;
        Student profileDetails = HttpManager.Get<Student>(url);
        Student newLastLoginDetails = new Student(username, profileDetails.classNumber,
                                                profileDetails.highestScore, profileDetails.numOfGamesCompleted,
                                                profileDetails.levelsUnlocked, profileDetails.subjectsTaken, DateTime.Now.ToString());
        var url_new = http_url + "update_userData";
        HttpManager.Post<Student>(url_new, newLastLoginDetails);
        SceneLoaderManager.LoadMainPageScene();
    }


}
