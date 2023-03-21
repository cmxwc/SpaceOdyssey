using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; // can load scene

public class DataManager : MonoBehaviour
{
    //public Student student;
    public static string username;
    public static string selectedSubject;
    public static int selectedTopic;
    public static int level = 1;
    public static int maxHp = 100;
    public static int health;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainPageUI");
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}