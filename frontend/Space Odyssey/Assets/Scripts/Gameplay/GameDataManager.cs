using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; // can load scene
using System.Linq;

public class GameDataManager : MonoBehaviour
{
    //public Student student;
    public List<Question> questionList;
    public int numberCorrect = 0;
    public int score = 0;
    public Dictionary<string, int> weakestLearningObjCount = new Dictionary<string, int>();
    public string dateOfGame;
    public bool completed;

    public void AddString(Dictionary<string, int> stringCounts, string str)
    {
        if (str != "")
        {
            if (stringCounts.ContainsKey(str))
            {
                // If the string is already in the dictionary, increment its count
                stringCounts[str]++;
            }
            else
            {
                // If the string is not in the dictionary, add it with a count of 1
                stringCounts.Add(str, 1);
            }
        }
    }

    public void updateWeakestLearningObjCount(string weakestLearningObj)
    {
        AddString(weakestLearningObjCount, weakestLearningObj);
    }

    public string getWeakestLearningObj()
    {
        if (weakestLearningObjCount.Count != 0)
        {
            foreach (KeyValuePair<string, int> pair in weakestLearningObjCount)
            {
                Debug.Log("Key: " + pair.Key + ", Value: " + pair.Value);
            }
            string keyWithHighestValue = weakestLearningObjCount.OrderByDescending(pair => pair.Value).First().Key;
            return keyWithHighestValue;
        }
        else
        {
            return "You've gotten everything correct! :)";
        }

    }
}