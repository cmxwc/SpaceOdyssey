using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject optionSelector;
    [SerializeField] GameObject optionDetails;
    [SerializeField] GameObject questionBox;

    [SerializeField] List<TextMeshProUGUI> actionText;
    [SerializeField] List<TextMeshProUGUI> optionText;
    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] TextMeshProUGUI detailsText;



    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator TypeQuestionDialog(string question)
    {
        questionText.text = "";
        foreach (var letter in question.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableOptionSelector(bool enabled)
    {
        optionSelector.SetActive(enabled);
        optionDetails.SetActive(enabled);
    }

    public void EnableQuestionBox(bool enabled, string questionStr)
    {
        questionBox.SetActive(enabled);
        questionText.text = questionStr;

    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionText.Count; i++)
        {
            if (i == selectedAction)
                actionText[i].color = highlightedColor;
            else
                actionText[i].color = Color.black;
        }
    }

    public void UpdateOptionSelection(int selectedOption, string currentOptionText)
    {
        for (int i = 0; i < optionText.Count; i++)
        {
            if (i == selectedOption)
                optionText[i].color = highlightedColor;
            else
                optionText[i].color = Color.black;
        }

        detailsText.text = currentOptionText;
    }

}
