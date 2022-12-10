using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadPersistent()
    {
        SceneManager.LoadScene("Persistent");
    }
    public void LoadMainPageScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public void LoadWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
    public void LoadProfileScene()
    {
        SceneManager.LoadScene("ProfileScene");
    }


}