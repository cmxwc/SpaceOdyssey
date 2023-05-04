using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuTest
{
    // MainMenu Tests 
    [UnityTest]
    public IEnumerator main_menu_ui_components_present()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(1);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to register
        usernameInput.text = "spaceman";
        passwordInput.text = "spaceman123";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(2);


        TMP_Text welcomeMsg = GameObject.Find("Title").GetComponent<TMP_Text>();
        Button profileButton = GameObject.Find("Profile Button").GetComponent<Button>();
        Button leaderboardButton = GameObject.Find("Leaderboard").GetComponent<Button>();
        Button achievementsButton = GameObject.Find("Achievements").GetComponent<Button>();
        Button startGameButton = GameObject.Find("Start Game Button").GetComponent<Button>();

        Assert.AreEqual(welcomeMsg.text, "Welcome back, spaceman");
        Assert.AreEqual(profileButton.GetType(), typeof(Button));
        Assert.AreEqual(leaderboardButton.GetType(), typeof(Button));
        Assert.AreEqual(achievementsButton.GetType(), typeof(Button));
        Assert.AreEqual(startGameButton.GetType(), typeof(Button));

    }


}
