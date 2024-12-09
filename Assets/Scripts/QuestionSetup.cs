using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using DG.Tweening;

public class QuestionSetup : MonoBehaviour
{
    [HideInInspector]
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
    private GameObject[] answerBox;
    [SerializeField]
    private GameObject[] mainGameObject;

    [SerializeField]
    private int correctAnswerChoice;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

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

    [SerializeField]
    private Image[] stars;

    [SerializeField]
    private Sprite activeStar;
    [SerializeField]
    private Sprite inactiveStar;

    private int[] scoreThresholds = { 60, 120, 200 };

    public bool isTransitioning = false;
    public bool isAnswered = false;

    private void Awake()
    {
        GetQuestionAssets();
    }

    public void Start()
    {
        SelectNewQuestion();
        SetQuestionValue();
        SetAnswerValue();
        AnimateAnswerBoxes();
        timer = timerDuration;
        isTimerRunning = true;
        isAnswered = false;
        
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

    public IEnumerator TransitionToNextQuestion(float delay)
    {
        if (isTransitioning) yield break;

        isTransitioning = true;

        yield return new WaitForSeconds(delay);

        if (questions.Count > 0)
        {
            Start();
        }
        else
        {
            Debug.Log("Habis Le...");
            CheckWinCondition();
        }

        isTransitioning = false;
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

    public void UpdateStars(int score)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (score >= scoreThresholds[i])
            {
                stars[i].sprite = activeStar;
            }
            else
            {
                stars[i].sprite = inactiveStar;
            }
        }
    }

    private void AnimateStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            float delay = i * 0.3f;
            stars[i].transform.localScale = Vector3.zero;
            stars[i].gameObject.SetActive(true);

            stars[i].transform.DOScale(0.4f, 0.5f).SetDelay(delay).SetEase(Ease.OutBack);
            Debug.Log("Hoshimaci Goooo");
        }
    }

    private IEnumerator DelayAnimateStars(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateStars(score);
        AnimateStars();
    }

    private void AnimateAnswerBoxes()
    {
        foreach (var box in answerBox)
        {
            box.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < answerBox.Length; i++)
        {
            float delay = i * 0.2f;
            answerBox[i].transform.DOScale(0.4f, 0.5f).SetDelay(delay).SetEase(Ease.OutBack);
        }
    }

    private void HideGameUI()
    {
        foreach (var obj in mainGameObject)
        {
            if (obj != null)
            {
                obj.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack);
            }
        }
    }

    private void HideGameUIWithDelay(GameObject panel, float delay)
    {
        StartCoroutine(HideAndShowCoroutine(panel, delay));
    }

    private IEnumerator HideAndShowCoroutine(GameObject panel, float delay)
    {
        HideGameUI();

        yield return new WaitForSeconds(delay);

        ShowWinLosePanel(panel);
    }

    private void ShowWinLosePanel(GameObject panel)
    {
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);
        panel.transform.DOScale(0.4f, 0.5f).SetEase(Ease.InBack);
        AudioManager.Instance.PlayWLSound();
        AudioManager.Instance.MuteBGM(true);
        StartCoroutine(UnmuteBGMAfterSound(2f));
    }

    private IEnumerator UnmuteBGMAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay); 
        AudioManager.Instance.MuteBGM(false); 
    }

    public void CheckWinCondition()
    {
        if (questions.Count == 0)
        {
            Debug.Log("Menang Le");

            if (winPanel != null)
            {
                HideGameUIWithDelay(winPanel, 0.5f);
            }
            if (finalScoreText != null)
            {
                finalScoreText.text = $"{score}";
            }

            StartCoroutine(DelayAnimateStars(1f));
        }
    }

    private void TriggerLoseCondition()
    {
        Debug.Log("Skill Issue");

        if (losePanel != null)
        {
            HideGameUIWithDelay(losePanel, 0.5f);
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = $"{score}";
        }

        StartCoroutine(DelayAnimateStars(1f));
    }

}
