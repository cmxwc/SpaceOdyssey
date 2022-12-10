using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StudentProfileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string username;
    public TextMeshProUGUI usernameLabel;
    void Start()
    {
        username = DataManager.username;
        displayWelcomeMessage();
    }

    public void displayWelcomeMessage()
    {
        usernameLabel.text = "Welcome back, " + username;
    }

}
