using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectTopicManager : MonoBehaviour
{
    public string username;
    private int selectedTopic = 1;
    private int numOfTopics = 3;
    public Image topicImage;
    public Sprite[] topicSprite;
    public TextMeshProUGUI topicLabel;

    void Start()
    {
        updateSelectedTopic();
    }

    public void updateSelectedTopic()
    {
        topicLabel.text = "Topic " + selectedTopic.ToString();
    }

    public void prevTopic()
    {
        selectedTopic -= 1;
        if (selectedTopic < 1)
        {
            selectedTopic = 1;
        }
        updateSelectedTopic();
    }
    public void nextTopic()
    {
        selectedTopic += 1;
        if (selectedTopic >= numOfTopics)
        {
            selectedTopic = numOfTopics;
        }
        updateSelectedTopic();
    }

    public void confirmSelectedPlanet()
    {
        DataManager.selectedTopic = selectedTopic;
        Debug.Log("Selected Topic " + DataManager.selectedTopic.ToString());
        DataManager.health = DataManager.maxHp;
        SceneLoaderManager.LoadGameScene();
    }
}
