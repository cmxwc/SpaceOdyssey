using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleSystemTest
{
    // Game Map Tests 
    [UnityTest]
    public IEnumerator battle_ui_components_present()
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

        Button startGameButton = GameObject.Find("Start Game Button").GetComponent<Button>();
        startGameButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        Button enterButton = GameObject.Find("Enter Button").GetComponent<Button>();
        enterButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
        startButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        GameObject battleSystem = GameObject.Find("BattleSystem");
    }

}
