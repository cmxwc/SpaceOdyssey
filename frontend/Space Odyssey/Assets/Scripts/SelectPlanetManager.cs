using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPlanetManager : MonoBehaviour
{

    public string username;
    public List<string> subjectsTaken;
    public Image planetImage;
    public Sprite[] planetSprite;
    private int selectedPlanet = 0;
    public TextMeshProUGUI planetLabel;

    void Start()
    {
        username = DataManager.username;
        getSubjectsTaken();
        planetLabel.text = subjectsTaken[selectedPlanet];
        planetImage.sprite = planetSprite[Planet.subjectIdMap[subjectsTaken[selectedPlanet]]];
    }

    public void getSubjectsTaken()
    {
        var url = HttpManager.http_url + "get_userData?username=" + username;
        Student profileDetails = HttpManager.Get<Student>(url);
        subjectsTaken = profileDetails.subjectsTaken;
    }

    public void updateSelectedPlanet()
    {
        planetLabel.text = subjectsTaken[selectedPlanet];
        planetImage.sprite = planetSprite[Planet.subjectIdMap[subjectsTaken[selectedPlanet]]];
    }
    public void prevPlanet()
    {
        selectedPlanet -= 1;
        if (selectedPlanet < 0)
        {
            selectedPlanet = 0;
        }
        updateSelectedPlanet();
    }
    public void nextPlanet()
    {
        selectedPlanet += 1;
        if (selectedPlanet >= subjectsTaken.Count - 1)
        {
            selectedPlanet = subjectsTaken.Count - 1;
        }
        updateSelectedPlanet();
    }

    public void confirmSelectedPlanet()
    {
        DataManager.selectedSubject = subjectsTaken[selectedPlanet];
        Debug.Log("Selected Planet " + DataManager.selectedSubject);
        SceneLoaderManager.LoadSelectTopicScene();
    }
}
