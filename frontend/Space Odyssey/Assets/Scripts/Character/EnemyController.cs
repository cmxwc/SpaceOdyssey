using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] string name;
    //[SerializeField] Sprite sprite;
    [SerializeField] GameObject exclaimation;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog dialogAfterBattle;
    [SerializeField] GameObject fov;
    [SerializeField] List<int> QuestionListIndex;
    [SerializeField] List<Question> selectedQuestions;
    [SerializeField] bool isBoss;

    //State
    bool battleLost = false;

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void Start()
    {
        SetFovRotation(character.Animator.DefaultDirection);
    }

    public void Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);
        if (!battleLost)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
            {
                Debug.Log("Start battle");
                GameController.Instance.StartEnemyBattle(this);
            }));
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialogAfterBattle));
        }
    }

    public IEnumerator TriggerEnemyBattle(PlayerController player)
    {
        // Show exclaimation
        exclaimation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        exclaimation.SetActive(false);

        // walk towards the player
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));
        Debug.Log(moveVec);
        yield return character.Move(moveVec);

        // Show dialog 
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
            Debug.Log("Start battle");
            GameController.Instance.StartEnemyBattle(this);
        }));

    }

    public void BattleLost()
    {
        battleLost = true;
        fov.gameObject.SetActive(false);
    }

    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
        else if (dir == FacingDirection.Up)
            angle = 180f;
        else if (dir == FacingDirection.Left)
            angle = 270f;

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public List<Question> GetSelectedQuestions()
    {
        var questionList = GameController.Instance.GetQuestions();
        List<Question> selectedQuestions = QuestionListIndex.Select(i => questionList[i]).ToList();
        return selectedQuestions;
    }

    public List<Question> Questions
    {
        get => GetSelectedQuestions();
    }
    public bool IsBoss
    {
        get => isBoss;
    }

    // public string Name
    // {
    //     get => name;
    // }
    //public Sprite Sprite
    //{
    //    get => sprite;
    //}


}
