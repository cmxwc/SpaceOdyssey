using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectGalaxyManager : MonoBehaviour
{

    public string username;
    public List<string> subjectsTaken;
    public Image galaxyImage;
    public Sprite[] galaxySprite;
    private int selectedGalaxy = 0;
    public TextMeshProUGUI galaxyLabel;

    void Start()
    {
        username = DataManager.username;
        getSubjectsTaken();
        galaxyLabel.text = subjectsTaken[selectedGalaxy];
        galaxyImage.sprite = galaxySprite[Galaxy.subjectIdMap[subjectsTaken[selectedGalaxy]]];
    }

    public void getSubjectsTaken()
    {
        var url = HttpManager.http_url + "get_userData?username=" + username;
        Student profileDetails = HttpManager.Get<Student>(url);
        subjectsTaken = profileDetails.subjectsTaken;
    }

    public void updateSelectedGalaxy()
    {
        galaxyLabel.text = subjectsTaken[selectedGalaxy];
        galaxyImage.sprite = galaxySprite[Galaxy.subjectIdMap[subjectsTaken[selectedGalaxy]]];
    }
    public void prevGalaxy()
    {
        selectedGalaxy -= 1;
        if (selectedGalaxy < 0)
        {
            selectedGalaxy = 0;
        }
        updateSelectedGalaxy();
    }
    public void nextGalaxy()
    {
        selectedGalaxy += 1;
        if (selectedGalaxy >= subjectsTaken.Count - 1)
        {
            selectedGalaxy = subjectsTaken.Count - 1;
        }
        updateSelectedGalaxy();
    }

    public void confirmSelectedGalaxy()
    {
        DataManager.selectedSubject = subjectsTaken[selectedGalaxy];
        Debug.Log("Selected Galaxy " + DataManager.selectedSubject);
        SceneLoaderManager.LoadSelectPlanetScene();
    }
}
