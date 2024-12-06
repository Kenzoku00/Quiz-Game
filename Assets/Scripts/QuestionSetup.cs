using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class QuestionSetup : MonoBehaviour
{
    [SerializeField]
    public List<QuestionData> questions;
    private QuestionData currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI categoryText;
    [SerializeField]
    private AnswerButton[] answerButtons;

    [SerializeField]
    private int correctAnswerChoice;

    private void Awake()
    {
        GetQuestionAssets();
    }

    public void Start()
    {
        SelectNewQuestion();
        SetQuestionValue();
        SetAnswerValue();
    }

    private void GetQuestionAssets()
    {
        questions = new List<QuestionData>(Resources.LoadAll<QuestionData>("Questions"));
    }

    private void SelectNewQuestion()
    {
        int randomQuestionIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];
        questions.RemoveAt(randomQuestionIndex);
    }

    private void SetQuestionValue()
    {
        questionText.text = currentQuestion.question;
        categoryText.text = currentQuestion.category;
    }

    private void SetAnswerValue()
    {
        List<Sprite> answers = RandomizeAnswers(new List<Sprite>(currentQuestion.answers));

        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool isCorrect = false;

            if (i == correctAnswerChoice)
            {
                isCorrect = true;
            }

            answerButtons[i].SetIsCorrect(isCorrect);
            answerButtons[i].SetAnswerImage(answers[i]);
        }
    }

    private List<Sprite> RandomizeAnswers(List<Sprite> originalList)
    {
        bool correctAnswerChosen = false;

        List<Sprite> newList = new List<Sprite>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int random = Random.Range(0, originalList.Count);

            if (random == 0 && !correctAnswerChosen)
            {
                correctAnswerChoice = i;
                correctAnswerChosen = true;
            }
             
            newList.Add(originalList[random]);
            originalList.RemoveAt(random);
        }

        return newList;
    }
}
