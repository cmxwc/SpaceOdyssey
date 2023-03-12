using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoginRegisterTest
{
    // Login/Register Tests 
    [UnityTest]
    public IEnumerator login_register_ui_components_present()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        Assert.AreEqual(usernameInput.GetType(), typeof(TMP_InputField));
        Assert.AreEqual(passwordInput.GetType(), typeof(TMP_InputField));

        yield return null;
    }

    [UnityTest]
    public IEnumerator username_password_input_correct()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();

        usernameInput.text = "testUsername";
        passwordInput.text = "Password123";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;

        Assert.AreEqual(usernameInput.text, "testUsername");
        Assert.AreEqual(passwordInput.text, "Password123");
    }

    [UnityTest]
    public IEnumerator login_register_input_empty()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(3);
        TextMeshProUGUI warningMessage1 = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage1.text, "Enter login details");


        Button registerButton = GameObject.Find("Register Button").GetComponent<Button>();
        registerButton.onClick.Invoke();
        yield return new WaitForSeconds(3);
        TextMeshProUGUI warningMessage2 = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage2.text, "Enter registration details");
    }


    // Register Tests
    [UnityTest]
    public IEnumerator error_message_existing_username()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button registerButton = GameObject.Find("Register Button").GetComponent<Button>();
        // use an existing username to register
        usernameInput.text = "fellybelly";
        passwordInput.text = "Password";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        registerButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        TextMeshProUGUI warningMessage = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage.text, "Username already exists!");
    }

    [UnityTest]
    public IEnumerator register_success_message()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        Button registerButton = GameObject.Find("Register Button").GetComponent<Button>();
        registerButton.onClick.Invoke();
        yield return new WaitForSeconds(3);
        TextMeshProUGUI warningMessage1 = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage1.text, "Enter registration details");
    }

    [UnityTest]
    public IEnumerator error_message_invalid_password()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button registerButton = GameObject.Find("Register Button").GetComponent<Button>();
        // use an invalid password to register
        usernameInput.text = "thebeststudent";
        passwordInput.text = "pass";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        registerButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        TextMeshProUGUI warningMessage = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage.text, "Enter password of at least 8 characters");
    }


    // Login Tests
    [UnityTest]
    public IEnumerator error_message_invalid_username()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to register
        usernameInput.text = "fellybellydoesnotexist";
        passwordInput.text = "Password";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        TextMeshProUGUI warningMessage2 = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage2.text, "Username does not exist!");
    }

    [UnityTest]
    public IEnumerator error_message_incorrect_password()
    {
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(3);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to register
        usernameInput.text = "fellybelly";
        passwordInput.text = "Password";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        TextMeshProUGUI warningMessage2 = GameObject.Find("Message Label").GetComponent<TextMeshProUGUI>();
        Assert.AreEqual(warningMessage2.text, "Wrong password, try again!");
    }





}
