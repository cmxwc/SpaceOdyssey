using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Planet
{
    public string subjectName;
    // Create a dictionary to map subjects to their subject ids
    public static Dictionary<string, int> subjectIdMap;
    static Planet()
    {
        subjectIdMap = new Dictionary<string, int>();
        // Add some key-value pairs to the dictionary
        subjectIdMap.Add("Maths", 0);
        subjectIdMap.Add("English", 1);
        subjectIdMap.Add("Geography", 2);

        // // Use the dictionary to map a color to its hexadecimal value
        // string redHex = subjectIdMap["Red"]; // redHex = "#FF0000"

        // // Use the dictionary to map a hexadecimal value to its color
        // string hex = "#FFFF00";
        // string color = subjectIdMap.FirstOrDefault(x => x.Value == hex).Key; // color = "Yellow"
    }

}