using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private bool isCorrect;
    [SerializeField] private Image answerImage;
    [SerializeField] public QuestionSetup questionSetup;

    public void SetAnswerImage(Sprite newImage)
    {
        answerImage.sprite = newImage;
    }

    public void SetIsCorrect(bool newBool)
    {
        isCorrect = newBool;
    }

    public void OnClick()
    {
        if (isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
        }
        else
        {
            Debug.Log("WRONG ANSWER");
        }

        if (questionSetup.questions.Count > 0)
        {
            questionSetup.Start();
        }
        else
        {
            Debug.Log("No more question available");
        }
    }
}
