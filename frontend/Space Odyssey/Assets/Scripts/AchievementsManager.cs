using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public string username;
    private HttpManager http;
    private string achievementDetails;
    [SerializeField] List<GameObject> completedAchievement;

    // Start is called before the first frame update
    void Start()
    {
        username = DataManager.username;
        achievementDetails = GetUserAchievements();
        if (achievementDetails != "No achievements yet")
            CheckAchievementCompleted(achievementDetails);
    }

    public string GetUserAchievements()
    {
        var url = HttpManager.http_url + "get_achievements?username=" + username;
        var achievementDetails = HttpManager.Task(url);
        Debug.Log(achievementDetails);

        return achievementDetails;
    }

    public void CheckAchievementCompleted(string achievementDetails)
    {
        if (achievementDetails.Contains("completeFirstGame"))
            completedAchievement[0].SetActive(false);
        if (achievementDetails.Contains("completeFullHealth"))
            completedAchievement[1].SetActive(false);
        if (achievementDetails.Contains("completeFiveGames"))
            completedAchievement[2].SetActive(false);
    }

}
