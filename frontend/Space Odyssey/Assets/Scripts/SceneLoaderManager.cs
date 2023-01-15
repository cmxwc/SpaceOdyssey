using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderManager : MonoBehaviour
{
    public static void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadPersistent()
    {
        SceneManager.LoadScene("Persistent");
    }
    public static void LoadMainPageScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public static void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public static void LoadWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
    public static void LoadProfileScene()
    {
        SceneManager.LoadScene("ProfileScene");
    }
    public static void LoadSelectPlanetScene()
    {
        SceneManager.LoadScene("SelectPlanetScene");
    }
    public static void LoadSelectTopicScene()
    {
        SceneManager.LoadScene("SelectTopicScene");
    }
    public static void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


}