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

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float timerDuration = 120f;
    private float timer;
    private bool isTimerRunning = false;

    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private GameObject winPanel;

    private void Awake()
    {
        GetQuestionAssets();
    }

    public void Start()
    {
        SelectNewQuestion();
        SetQuestionValue();
        SetAnswerValue();
        timer = timerDuration;
        isTimerRunning = true;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            RunTimer();
        }
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

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score: {score}");

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void CheckWinCondition()
    {
        if (questions.Count == 0)
        {
            Debug.Log("Menang Le");
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }
        }
    }

    private void RunTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            timer = 0;
            isTimerRunning = false;
            Debug.Log("Za Warudo");
            TriggerLoseCondition();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void TriggerLoseCondition()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        Debug.Log("Skill Issue");
    }
}
