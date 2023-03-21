using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class QuestionBattleRecord
{
    public QuestionBattleRecord(string username, string questionSubject, int questionId, bool correct)
    {
        this.username = username;
        this.questionId = questionId;
        this.questionSubject = questionSubject;
        this.correct = correct;
    }

    public string username { get; set; }
    public int questionId { get; set; }
    public string questionSubject { get; set; }
    public bool correct { get; set; }


}