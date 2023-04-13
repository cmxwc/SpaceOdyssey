using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public enum GameState { FreeRoam, Battle, Dialog, Cutscene }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] BattleDialogBox dialogBox;
    GameState state;
    private string currentScene;

    public List<Question> allQuestionList;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentScene = SceneLoaderManager.CurrentScene();
        GameDataManager.startTime = DateTime.Now;

        int difficultyLevel = currentDifficultyLevel();
        Debug.Log("difficultyLevel is" + difficultyLevel.ToString());
        GameDataManager.questionList = QuestionManager.getQuestionDataBySubjectTopicDifficulty(DataManager.selectedSubject, DataManager.selectedTopic, difficultyLevel, DataManager.year);
        allQuestionList = GameDataManager.questionList;
        SelectQuestionsRandomly();
        Debug.Log("Questions for difficulty level have been retrived");

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
        return GameDataManager.questionList;
    }

    // In each difficulty level game map, there are 6 enemies with 2 questions each.
    // For each difficulty level, randomly select 12 questions to assign to the 6 enemies.
    private void SelectQuestionsRandomly()
    {
        List<Question> questionList = allQuestionList;
        int numOfQuestionsToSelect = 12;
        System.Random rnd = new System.Random();
        List<Question> randomQuestions = new List<Question>();
        for (int i = 0; i < numOfQuestionsToSelect; i++)
        {
            int randomIndex = rnd.Next(0, questionList.Count);
            randomQuestions.Add(questionList[randomIndex]);
            questionList.RemoveAt(randomIndex);
        }
        allQuestionList = randomQuestions;
        Debug.Log(allQuestionList.Count);
        Debug.Log(allQuestionList);
    }
    public void UpdateAfterEachQuestion(int correct, int score, string weakestLearningObj = "")
    {
        GameDataManager.numberCorrectLevel += correct;
        GameDataManager.numberCorrectGame += correct;
        GameDataManager.score += score;
        GameDataManager.updateWeakestLearningObjCount(weakestLearningObj);
        Debug.Log("Total correct so far (level): " + GameDataManager.numberCorrectLevel + "Total correct so far (game): " + GameDataManager.numberCorrectGame + " Weakest: " + GameDataManager.getWeakestLearningObj());
    }

    public void HandleLevelComplete(bool died)
    {
        if (died)
        {
            StartCoroutine(ShowLoadingScreen(false, "", ""));
            // SceneLoaderManager.LoadSummaryScene();
        }
        else
        {
            int numberCorrectLevel = GameDataManager.numberCorrectLevel;
            // TODO change this if showing demo
            // numberCorrectLevel = 10;
            if (numberCorrectLevel >= 8)
            {
                // Load next level or scene
                GameDataManager.numberCorrectLevel = 0;
                if (SceneLoaderManager.CurrentScene() == "GameScene 1")
                {
                    Debug.Log("Good job, heading to level 2...");
                    StartCoroutine(ShowLoadingScreen(true, "level 2", "GameScene 2"));
                    // SceneLoaderManager.LoadGameScene2();
                }
                else if (SceneLoaderManager.CurrentScene() == "GameScene 2")
                {
                    Debug.Log("Good job, heading to level 3...");
                    StartCoroutine(ShowLoadingScreen(true, "level 3", "GameScene 3"));
                    // SceneLoaderManager.LoadGameScene3();
                }
                else if (SceneLoaderManager.CurrentScene() == "GameScene 3")
                {
                    Debug.Log("Good job, heading to game summary...");
                    StartCoroutine(ShowLoadingScreen(true, "game summary", "SummaryScene"));
                    // SceneLoaderManager.LoadSummaryScene();
                }
            }
            else
            {
                StartCoroutine(ShowLoadingScreen(false, "", ""));
                // SceneLoaderManager.LoadSummaryScene();
            }
        }

    }
    public IEnumerator ShowLoadingScreen(bool win, string level, string nextScene)
    {
        worldCamera.gameObject.SetActive(false);
        LoadingScreen.gameObject.SetActive(true);
        if (win)
        {
            yield return StartCoroutine(dialogBox.TypeDialog("Good job, you have completed this level! Heading to " + level + "..."));
            yield return new WaitForSeconds(2f);
            SceneLoaderManager.Load(nextScene);
        }
        else
        {
            yield return StartCoroutine(dialogBox.TypeDialog("Good try, but you have not defeated enough enemies :(( Try again!!! Loading game summary..."));
            yield return new WaitForSeconds(2f);
            SceneLoaderManager.LoadSummaryScene();
        }

    }

    public int currentDifficultyLevel()
    {
        currentScene = SceneLoaderManager.CurrentScene();
        int difficultyLevel = 1;
        if (currentScene == "GameScene 2")
            difficultyLevel = 2;
        else if (currentScene == "GameScene 3")
            difficultyLevel = 3;
        return difficultyLevel;
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
