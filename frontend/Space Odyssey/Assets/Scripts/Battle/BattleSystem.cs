using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, PlayerAction, EnemyQuestion, PlayerOption, Busy }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleDialogBox dialogBox;
    //[SerializeField] Image enemyImage;
    //[SerializeField] Image playerImage;

    public event Action<bool> OnBattleOver;
    public List<Question> questionList;
    public int currentQuestion = 0;
    public int selectedOption;
    // public Question currentQuestion;
    int enemyHp = 100;

    int currentAction;
    int currentOption;

    PlayerController player;
    EnemyController enemy;

    BattleState state;


    public void StartEnemyBattle()
    {
        player = GetComponent<PlayerController>();
        enemy = GetComponent<EnemyController>();

        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        DataManager.health = 100;
        questionList = getQuestionDataBySubjectTopic("English", 1);
        // getQuestionDataBySubjectTopic(DataManager.selectedSubject, DataManager.selectedTopic);
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData();
        enemyHud.SetEnemy();

        yield return StartCoroutine(dialogBox.TypeDialog("An enemy has challenged you to a duel!"));

        EnemyQuestion();
        PlayerAction();
    }

    public List<Question> getQuestionDataBySubjectTopic(string subject, int topic)
    {
        var url = HttpManager.http_url + "get_question_by_subject?subject=" + subject + "&topic=" + topic;
        return HttpManager.Get<List<Question>>(url);
    }

    void EnemyQuestion()
    {
        state = BattleState.EnemyQuestion;
        dialogBox.EnableQuestionBox(true, questionList[currentQuestion].questionText);
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

    // Dialog Box response after player has selected option
    IEnumerator PerformPlayerOption()
    {
        selectedOption = currentOption + 1;
        yield return dialogBox.TypeDialog($"You selected option {selectedOption}.");

        if (ValidateAnswer(currentOption))
        {
            yield return dialogBox.TypeDialog("Your answer is correct! Enemy has taken damage!");
            playerUnit.PlayAttackAnimation();
            enemyUnit.PlayHitAnimation();
            yield return TakeDamage("enemy");
        }
        else
        {
            yield return dialogBox.TypeDialog("Your answer is incorrect! You have taken damage!");
            enemyUnit.PlayAttackAnimation();
            playerUnit.PlayHitAnimation();
            yield return TakeDamage("player");
        }

        yield return new WaitForSeconds(2f);

        if (enemyHp > 0 && DataManager.health > 0)
        {
            currentQuestion++;
            EnemyQuestion();
            PlayerAction();
        }
        else if (enemyHp <= 0)
        {
            yield return dialogBox.TypeDialog("Enemy has fainted!");
            enemyUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else if (DataManager.health <= 0)
        {
            yield return dialogBox.TypeDialog("Your health has reached zero! You have fainted!");
            playerUnit.PlayFaintAnimation();
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }

    }

    // Validate the answer submitted by user
    public bool ValidateAnswer(int selectedOption)
    {
        if (selectedOption == questionList[currentQuestion].questionAnsIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Function for enemy or player to take damage
    public IEnumerator TakeDamage(string player)
    {
        if (player == "enemy")
        {
            int numQns = 3;
            int damage = 100 / numQns;

            enemyHp -= damage;
            yield return enemyHud.UpdateHP(enemyHp, 100);
        }
        else
        {
            int damage = 10;
            DataManager.health -= damage;

            yield return playerHud.UpdateHP(DataManager.health, DataManager.maxHp);
        }
    }


    public void HandleUpdate()
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

        if (Input.GetKeyDown(KeyCode.Return))
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

        dialogBox.UpdateOptionSelection(currentOption, questionList[currentQuestion].questionAnsText[currentOption]);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogBox.EnableOptionSelector(false);
            dialogBox.EnableQuestionBox(false, "");
            dialogBox.EnableDialogText(true);

            StartCoroutine(PerformPlayerOption());
        }
    }
}
