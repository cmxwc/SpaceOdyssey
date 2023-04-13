using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Question
{
    public Question(int questionId, string questionSubject, int year, int questionTopic, int questionDifficulty, string questionText, int questionAns, string option1, string option2, string option3, string option4, string questionLearningObj)
    {
        this.questionId = questionId;
        this.questionSubject = questionSubject;
        this.year = year;
        this.questionTopic = questionTopic;
        this.questionDifficulty = questionDifficulty;
        this.questionText = questionText;
        this.questionAns = questionAns;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
        this.option4 = option4;
        this.questionLearningObj = questionLearningObj;
    }

    public int questionId { get; set; }
    public string questionSubject { get; set; }
    public int year { get; set; }
    public int questionTopic { get; set; }
    public int questionDifficulty { get; set; }
    public string questionText { get; set; }
    public int questionAns { get; set; }
    public string option1 { get; set; }
    public string option2 { get; set; }
    public string option3 { get; set; }
    public string option4 { get; set; }
    public string questionLearningObj { get; set; }

}