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
        var url = HttpManager.http_url + "get_question_by_subject_topic?subject=" + subject + "&topic=" + topic;
        return HttpManager.Get<List<Question>>(url);
    }

    public static List<Question> getQuestionDataBySubjectTopicDifficulty(string subject, int topic, int difficulty)
    {
        var url = HttpManager.http_url + "get_question_by_subject_topic_difficulty?subject=" + subject + "&topic=" + topic + "&difficulty=" + difficulty;
        return HttpManager.Get<List<Question>>(url);
    }

}
