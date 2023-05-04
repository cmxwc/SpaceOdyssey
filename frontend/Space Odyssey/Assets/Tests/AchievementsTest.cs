using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AchievementsTest
{
    // Achievements Tests 
    [UnityTest]
    public IEnumerator achievements_ui_components_present()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        SceneManager.LoadScene("AchievementsScene");
        yield return new WaitForSeconds(1);
        TMP_Text badge1 = GameObject.Find("Badge1").GetComponentInChildren<TMP_Text>();
        TMP_Text badge2 = GameObject.Find("Badge2").GetComponentInChildren<TMP_Text>();
        TMP_Text badge3 = GameObject.Find("Badge3").GetComponentInChildren<TMP_Text>();

        Assert.AreEqual(badge1.text, "Complete your first game");
        Assert.AreEqual(badge2.text, "Complete a game with full health");
        Assert.AreEqual(badge3.text, "Complete 5 games");

    }

    [UnityTest]
    public IEnumerator achievements_correctly_indicated()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(1);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to register
        usernameInput.text = "cholelele";
        passwordInput.text = "cholelele123";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(2);

        Button achievementsButton = GameObject.Find("Achievements").GetComponent<Button>();
        achievementsButton.onClick.Invoke();
        yield return new WaitForSeconds(2);


        Image badge1 = GameObject.Find("Badge1").GetComponentInChildren<Image>();
        Image badge2 = GameObject.Find("Badge2").GetComponentInChildren<Image>();
        Image badge3 = GameObject.Find("Badge3").GetComponentInChildren<Image>();

        Assert.AreEqual(badge1.enabled, true);
        Assert.AreEqual(badge2.enabled, true);
        Assert.AreEqual(badge3.enabled, true);

    }

}
