using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, EnemyQuestion, PlayerOption, Busy }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    // [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;


    int currentAction;
    int currentOption;

    BattleState state;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        getQuestionDataBySubjectTopic("English", 1);
        // getQuestionDataBySubjectTopic(DataManager.selectedSubject, DataManager.selectedTopic);
        playerHud.SetData();
        enemyHud.SetEnemy();

        yield return StartCoroutine(dialogBox.TypeDialog("Enemy #1 has challenged you to a duel!"));
        yield return new WaitForSeconds(1f);

        EnemyQuestion();
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }

    public void getQuestionDataBySubjectTopic(string subject, int topic)
    {
        var url = HttpManager.http_url + "get_question_by_subject?subject=" + subject + "&topic=" + topic;
        List<Question> questionList = HttpManager.Get<List<Question>>(url);
        Debug.Log(questionList[0].questionText);
    }
    void EnemyQuestion()
    {
        state = BattleState.EnemyQuestion;
        StartCoroutine(dialogBox.TypeDialog("What does 1+1 equal to?"));
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an option:"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerOption()
    {
        state = BattleState.PlayerOption;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableOptionSelector(true);
    }

    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerOption)
        {
            HandleOptionSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                // Fight
                PlayerOption();
            }
            else if (currentAction == 1)
            {
                // Run
            }
        }
    }

    void HandleOptionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentOption < 3)
                ++currentOption;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentOption > 0)
                --currentOption;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOption < 2)
                currentOption += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentOption > 1)
                currentOption -= 2;
        }
    }
}
