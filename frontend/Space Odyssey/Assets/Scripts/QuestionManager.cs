using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public static List<Question> getQuestionDataBySubjectTopic(string subject, int topic)
    {
        var url = HttpManager.http_url + "get_question_by_subject?subject=" + subject + "&topic=" + topic;
        return HttpManager.Get<List<Question>>(url);
    }


    // public void getQuestionDataBySubjectTopic(string subject, int topic)
    // {
    //     var url = HttpManager.http_url + "get_question_by_subject?subject=" + subject + "topic=" + topic;
    //     List<Question> questionList = HttpManager.Get<List<Question>>(url);
    //     Debug.Log(questionList[0]);
    // }
    // public void displayUserData()
    // {
    //     var url = http_url + "get_userData?username=" + username;
    //     Student profileDetails = HttpManager.Get<Student>(url);  // Must specify the <TResultType> when calling the method
    //     usernameLabel.text = username;
    //     classLabel.text = profileDetails.classNumber.ToString();
    //     highestScoreLabel.text = profileDetails.highestScore.ToString();
    //     numOfGamesCompletedLabel.text = profileDetails.numOfGamesCompleted.ToString();
    //     string allSubjects = string.Join(", ", profileDetails.subjectsTaken);  // change list to a concatenated string
    //     subjectsTakenLabel.text = allSubjects;
    //     lastLoginLabel.text = profileDetails.lastLoginDay;
    // }

    // // Student Last Login Details Class
    // public class StudentLastLoginDetails
    // {
    //     public StudentLastLoginDetails(string username, string lastLoginDay)
    //     {
    //         this.username = username;
    //         this.lastLoginDay = lastLoginDay;
    //     }

    //     public string username { get; set; }
    //     public string lastLoginDay { get; set; }

    // }
    // public void logOut()
    // {
    //     var url = HttpManager.http_url + "update_userData_login";
    //     StudentLastLoginDetails newLastLoginDetails = new StudentLastLoginDetails(username, DateTime.Now.ToString());
    //     var response = HttpManager.Post(url, newLastLoginDetails);
    //     Debug.Log("post" + response);
    //     SceneLoaderManager.LoadMainPageScene();
    // }


}
