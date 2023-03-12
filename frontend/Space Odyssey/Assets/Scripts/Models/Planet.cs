using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Planet
{
    public string topicName;
    // Create a dictionary to map subjects to their subject ids
    public static Dictionary<string, int> topicIdMap;
    static Planet()
    {
        topicIdMap = new Dictionary<string, int>();
        // Add some key-value pairs to the dictionary
        topicIdMap.Add("1", 0);
        topicIdMap.Add("2", 1);
        topicIdMap.Add("3", 2);
    }

}