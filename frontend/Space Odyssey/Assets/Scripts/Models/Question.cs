using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Question
{
    public Question(int questionId, string questionSubject, int questionTopic, string questionText, int questionAnsIndex, List<string> questionAnsText, int questionLearningObj)
    {
        this.questionId = questionId;
        this.questionSubject = questionSubject;
        this.questionTopic = questionTopic;
        this.questionText = questionText;
        this.questionAnsIndex = questionAnsIndex;
        this.questionAnsText = questionAnsText;
        this.questionLearningObj = questionLearningObj;
    }

    public int questionId { get; set; }
    public string questionSubject { get; set; }
    public int questionTopic { get; set; }
    public string questionText { get; set; }
    public int questionAnsIndex { get; set; }
    public List<string> questionAnsText { get; set; }
    public int questionLearningObj { get; set; }

}