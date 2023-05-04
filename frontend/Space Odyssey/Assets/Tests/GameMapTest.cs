using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMapTest
{
    // Game Map Tests 
    [UnityTest]
    public IEnumerator correct_number_of_enemies()
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

        GameObject enemy1 = GameObject.Find("Enemy1");
        GameObject enemy2 = GameObject.Find("Enemy2");
        GameObject enemy3 = GameObject.Find("Enemy3");
        GameObject enemy4 = GameObject.Find("Enemy4");
        GameObject enemy5 = GameObject.Find("Enemy5");
        GameObject boss = GameObject.Find("Enemy Boss");

        Assert.IsNotNull(enemy1, "Failed to find enemy1");
        Assert.IsNotNull(enemy2, "Failed to find enemy2");
        Assert.IsNotNull(enemy3, "Failed to find enemy3");
        Assert.IsNotNull(enemy4, "Failed to find enemy4");
        Assert.IsNotNull(enemy5, "Failed to find enemy5");
        Assert.IsNotNull(boss, "Failed to find boss");
    }


    [UnityTest]
    public IEnumerator player_avatar_is_present()
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

        GameObject player = GameObject.Find("Player");
        Assert.IsNotNull(player, "Failed to find player");
    }


    [UnityTest]
    public IEnumerator each_enemy_assigned_questions()
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

        GameObject enemy1 = GameObject.Find("Enemy1");
        GameObject enemy2 = GameObject.Find("Enemy2");
        GameObject enemy3 = GameObject.Find("Enemy3");
        GameObject enemy4 = GameObject.Find("Enemy4");
        GameObject enemy5 = GameObject.Find("Enemy5");
        GameObject boss = GameObject.Find("Enemy Boss");

        Assert.IsNotNull(enemy1, "Failed to find enemy1");
        Assert.IsNotNull(enemy2, "Failed to find enemy2");
        Assert.IsNotNull(enemy3, "Failed to find enemy3");
        Assert.IsNotNull(enemy4, "Failed to find enemy4");
        Assert.IsNotNull(enemy5, "Failed to find enemy5");
        Assert.IsNotNull(boss, "Failed to find boss");
    }






}
