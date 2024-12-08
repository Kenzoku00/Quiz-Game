using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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
            questionSetup.AddScore(10);
            AnimateCorrectAnswer();
        }
        else
        {
            Debug.Log("WRONG ANSWER");
            AnimateWrongAnswer();
        }
    }

    private void AnimateCorrectAnswer()
    {
        answerImage.transform.DOScale(1.2f, 0.25f).OnKill(() =>
        {
            answerImage.transform.DOScale(1f, 0.25f).OnKill(() =>
            {
                if (questionSetup.questions.Count > 0)
                {
                    questionSetup.Start();
                }
                else
                {
                    Debug.Log("Habis Le...");
                    questionSetup.CheckWinCondition();
                }
            });
        });
    }

    private void AnimateWrongAnswer()
    {
        answerImage.transform.DOShakePosition(0.5f, 20f, 50, 90, false, true).OnKill(() =>
        {
            if (questionSetup.questions.Count > 0)
            {
                questionSetup.Start();
            }
            else
            {
                Debug.Log("Habis Le...");
                questionSetup.CheckWinCondition();
            }
        });
    }
}
