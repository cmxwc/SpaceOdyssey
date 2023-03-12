using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Galaxy
{
    public string subjectName;
    // Create a dictionary to map subjects to their subject ids
    public static Dictionary<string, int> subjectIdMap;
    static Galaxy()
    {
        subjectIdMap = new Dictionary<string, int>();
        // Add some key-value pairs to the dictionary
        subjectIdMap.Add("Maths", 0);
        subjectIdMap.Add("English", 1);
        subjectIdMap.Add("Geography", 2);
    }

}