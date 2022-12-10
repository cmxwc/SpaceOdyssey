using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LoginAndRegister : MonoBehaviour
{
    private string usernameInput;
    private bool usernameValid;
    private bool passwordValid;
    private string passwordEncrypted;
    public PasswordManager pwd;
    private HttpManager http;
    // private SceneLoadermanager scene;
    public Student student;
    private DataManager dataController;

    // Student Login Details Class
    public class StudentLoginDetails
    {
        public StudentLoginDetails(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username { get; set; }
        public string password { get; set; }

    }

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void IsUsernameValid()
    {
        // Ensure username has at least 6 characters and is only upper or lowercase letters
        if ((usernameInput.Length >= 6) & (Regex.IsMatch(usernameInput, @"^[a-zA-Z]+$")))
        {
            usernameValid = true;
        }
        else if (usernameInput == "admin")
        {
            usernameValid = true;
        }
        else
        {
            usernameValid = false;
        }

    }

    public void IsPasswordValid()
    {
        // Ensure password is at least 8 characters
        if (passwordEncrypted.Length >= 8)
        {
            passwordValid = true;
        }
        else if (passwordEncrypted == "admin")
        {
            passwordValid = true;
        }
        else
        {
            passwordValid = false;
        }

    }


    public void ReadUsernameInput(string s)
    {
        Debug.log(s);
        usernameInput = s;
        if (usernameValid)
        {
            usernameInput = s;
        }
        else
        {
            usernameInput = "";
            MessageLabel.text = "Enter username of at least 6 characters";
        }

    }

    public void ReadPasswordInput(string s)
    {
        Debug.Log(s);
        passwordEncrypted = s;
        IsPasswordValid();
        if (passwordValid)
        {
            pwd = new PasswordManager();
            passwordEncrypted = pwd.ConvertToEncrypt(s);
            Debug.Log(passwordEncrypted);
        }
        else
        {
            passwordEncrypted = "";
            MessageLabel.text = "Enter password of at least 8 chars";
        }
    }


    private StudentLoginDetails studentLogin;
    public void Login()
    {
        // scene = new SceneLoaderManager();
        pwd = new PasswordManager();
        var temp = pwd.ConvertToEncrypt("admin");

        if (usernameInput == null & passwordEncrypted == null)
        {
            MessageLabel.text = "Enter login details";
        }
        else if (usernameInput == null & passwordValid)
        {
            MessageLabel.text = "Enter username";
        }
        else if (passwordEncrypted == null & usernameValid)
        {
            MessageLabel.text = "Enter password";
        }

        if (usernameValid & passwordValid)
        {
            if ((usernameInput == "admin") & (passwordEncrypted == temp))
            {
                CreateNewStudentData();
                SaveUsername();
                scene.LoadStudentWelcomeUI();
            }
            else
            {
                studentLogin = new StudentLoginDetails(usernameInput, passwordEncrypted);
                http = new HttpManager();
                // TODO Change url
                // var url = "http://172.21.148.165/login_student";
                // var response = http.Post(url, studentLogin);
                // Debug.Log(response);
                // response = response.Substring(1, response.Length - 2);
                // MessageLabel.text = response;

                // if (response == "Successfully authenticated")
                // {
                //     SaveUsername();
                //     scene.LoadStudentWelcomeUI();
                // }

            }
        }
    }



    public void CreateNewStudentData()
    {
        var levelsUnlockedList = new List<int> { 0, 1 };
        var subjectsTakenList = new List<string> { "Maths", "English" };
        http = new HttpManager();
        // TODO change url
        // var url = "http://172.21.148.165/add_userData";
        // student = new Student(usernameInput, 0, 0, 0, levelsUnlockedList, subjectsTakenList, DateTime.Now.ToString());
        // var response = http.Post(url, student); // post to backend studentdata
        // Debug.Log("post " + response);

    }

    public void SaveUsername()
    {
        dataController = FindObjectOfType<DataManager>();
        dataController.username = usernameInput;
    }


}
