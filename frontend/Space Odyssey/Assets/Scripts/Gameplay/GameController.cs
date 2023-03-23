using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState { FreeRoam, Battle, Dialog, Cutscene }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    GameState state;

    public List<Question> allQuestionList;
    public GameDataManager gameDataManager;

    public DateTime startTime;


    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startTime = DateTime.Now;

        gameDataManager = new GameDataManager();
        // GameDataManager.questionList = QuestionManager.getQuestionDataBySubjectTopic(DataManager.selectedSubject, DataManager.selectedTopic);
        gameDataManager.questionList = QuestionManager.getQuestionDataBySubjectTopic("English", 1);
        allQuestionList = gameDataManager.questionList;
        Debug.Log("Questions have been retrived");
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;

        playerController.OnEnterEnemysView += (Collider2D enemyCollider) =>
        {
            var enemy = enemyCollider.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                state = GameState.Cutscene;
                StartCoroutine(enemy.TriggerEnemyBattle(playerController));
            }
        };

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    EnemyController enemy;

    public void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }
    public void StartEnemyBattle(EnemyController enemy)
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        this.enemy = enemy;

        battleSystem.StartEnemyBattle(enemy);
    }

    void EndBattle(bool won)
    {
        if (enemy != null && won == true)
        {
            enemy.BattleLost();
            enemy = null;
        }
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    public List<Question> GetQuestions()
    {
        return gameDataManager.questionList;
    }
    public void UpdateAfterEachQuestion(int correct, int score, string weakestLearningObj = "")
    {
        gameDataManager.numberCorrect += correct;
        gameDataManager.score += score;
        gameDataManager.updateWeakestLearningObjCount(weakestLearningObj);
        Debug.Log("Total correct so far: " + gameDataManager.numberCorrect + " Weakest: " + gameDataManager.getWeakestLearningObj());
    }
    public void PostAfterGame(bool completed)
    {
        DateTime endTime = DateTime.Now;
        double duration = Math.Round((endTime - startTime).TotalMinutes, 2);
        GameRecord gameRecord = new GameRecord(DataManager.username, gameDataManager.score, gameDataManager.numberCorrect, gameDataManager.getWeakestLearningObj(), DateTime.Now.ToString(), completed, duration);
        var url = HttpManager.http_url + "add_gamerecord";
        var response = HttpManager.Post(url, gameRecord);
        Debug.Log(response);

        SceneLoaderManager.LoadSummaryScene();
    }


    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
