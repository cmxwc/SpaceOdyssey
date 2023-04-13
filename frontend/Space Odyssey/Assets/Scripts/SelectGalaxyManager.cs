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
    private int selectedYear = 1;
    public TextMeshProUGUI galaxyLabel;
    public TextMeshProUGUI yearLabel;

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

    public void updateSelectedYear()
    {
        yearLabel.text = "YEAR " + selectedYear;
    }
    public void prevYear()
    {
        selectedYear -= 1;
        if (selectedYear < 1)
        {
            selectedYear = 1;
        }
        updateSelectedYear();
    }
    public void nextYear()
    {
        selectedYear += 1;
        if (selectedYear >= 4)
        {
            selectedYear = 4;
        }
        updateSelectedYear();
    }

    public void confirmSelectedGalaxy()
    {
        DataManager.selectedSubject = subjectsTaken[selectedGalaxy];
        DataManager.year = selectedYear;
        Debug.Log("Selected Galaxy " + DataManager.selectedSubject);
        Debug.Log("Selected Year " + DataManager.year);
        SceneLoaderManager.LoadSelectPlanetScene();
    }
}
