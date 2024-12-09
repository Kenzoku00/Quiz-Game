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

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;

    [SerializeField] private Image correctPopUpImage;
    [SerializeField] private Image wrongPopUpImage;

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
        if (questionSetup.isTransitioning) return;

        if (questionSetup.isAnswered) return;

        questionSetup.isAnswered = true;

        if (isCorrect)
        {
            Debug.Log("CORRECT ANSWER");
            questionSetup.AddScore(10);
            AnimateCorrectAnswer();
            AudioManager.Instance.PlaySFX(correctSound);
            ShowPopUp(correctPopUpImage);
        }
        else
        {
            Debug.Log("WRONG ANSWER");
            AnimateWrongAnswer();
            AudioManager.Instance.PlaySFX(wrongSound);
            ShowPopUp(wrongPopUpImage);
        }
    }

    private void ShowPopUp(Image popUpImage)
    {
        if (popUpImage != null)
        {
            popUpImage.gameObject.SetActive(true);
            popUpImage.transform.localScale = Vector3.zero;
            popUpImage.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnKill(() =>
            {
                StartCoroutine(HidePopUp(popUpImage));
            });
        }
    }

    private IEnumerator HidePopUp(Image popUpImage)
    {
        yield return new WaitForSeconds(1f); 

        popUpImage.transform.DOScale(0f, 0.3f).OnKill(() =>
        {
            popUpImage.gameObject.SetActive(false);
        });
    }

    private void AnimateCorrectAnswer()
    {
        answerImage.transform.DOScale(1.2f, 0.25f).OnKill(() =>
        {
            answerImage.transform.DOScale(1f, 0.25f).OnKill(() =>
            {
                questionSetup.StartCoroutine(questionSetup.TransitionToNextQuestion(0.5f));
            });
        });
    }

    private void AnimateWrongAnswer()
    {
        answerImage.transform.DOShakePosition(0.5f, 20f, 50, 90, false, true).OnKill(() =>
        {
            questionSetup.StartCoroutine(questionSetup.TransitionToNextQuestion(0.5f));
        });
    }
}
