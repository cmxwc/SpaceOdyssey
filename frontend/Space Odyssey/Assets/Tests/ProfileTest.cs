using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ProfileTest
{
    // Profile Tests 
    [UnityTest]
    public IEnumerator profile_ui_components_present()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(1);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to login
        usernameInput.text = "spaceman";
        passwordInput.text = "spaceman123";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(2);

        Button profileButton = GameObject.Find("Profile Button").GetComponent<Button>();
        profileButton.onClick.Invoke();
        yield return new WaitForSeconds(2);

        TMP_Text usernameValue = GameObject.Find("Username Value").GetComponent<TMP_Text>();
        TMP_Text classValue = GameObject.Find("Class Value").GetComponent<TMP_Text>();

        Assert.AreEqual(usernameValue.GetType(), typeof(TextMeshProUGUI));
        Assert.AreEqual(classValue.GetType(), typeof(TextMeshProUGUI));

    }


    [UnityTest]
    public IEnumerator check_all_info_correct()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("LoginScene");
        yield return new WaitForSeconds(1);

        TMP_InputField passwordInput = GameObject.Find("Password Input").GetComponent<TMP_InputField>();
        TMP_InputField usernameInput = GameObject.Find("Username Input").GetComponent<TMP_InputField>();
        Button loginButton = GameObject.Find("Login Button").GetComponent<Button>();
        // use an existing username to login
        usernameInput.text = "cholelele";
        passwordInput.text = "cholelele123";
        usernameInput.onEndEdit.Invoke(usernameInput.text);
        passwordInput.onEndEdit.Invoke(passwordInput.text);
        yield return null;
        loginButton.onClick.Invoke();
        yield return new WaitForSeconds(2);

        Button profileButton = GameObject.Find("Profile Button").GetComponent<Button>();
        profileButton.onClick.Invoke();
        yield return new WaitForSeconds(2);

        TMP_Text usernameValue = GameObject.Find("Username Value").GetComponent<TMP_Text>();
        TMP_Text classValue = GameObject.Find("Class Value").GetComponent<TMP_Text>();

        Assert.AreEqual(usernameValue.text, "cholelele");
        Assert.AreEqual(classValue.text, "1");

    }


}
