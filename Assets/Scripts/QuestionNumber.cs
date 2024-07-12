using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionNumber : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionNumber;
        public string questionText;
        public string list1Text;
        public string list2Text;
    }

    [System.Serializable]
    public class PlanetSlots
    {
        public GameObject planet;
        public List<GameObject> correctSlots;
    }

    public TextMeshProUGUI questionNumberTMP;
    public TextMeshProUGUI questionTMP;
    public TextMeshProUGUI list1TMP;
    public TextMeshProUGUI list2TMP;
    public TextMeshProUGUI resultsTMP;

    public GameObject list1;
    public GameObject list2;

    public Button startButton;
    public Button doneButton;
    public Button nextButton;

    public List<Question> questions;
    private int currentQuestionIndex = 0;
    public List<PlanetSlots> planetSlots;

    private string result1 = "Correct answer is Venus, Mercury, Mars, Earth, Neptune, Uranus, Saturn, and Jupiter.";
    private string result2 = "Correct answer is Mercury, Mars, Venus, Earth, Neptune, Uranus, Saturn, and Jupiter.";

    void Start()
    {
        list1.SetActive(false);
        list2.SetActive(false);
        questionNumberTMP.gameObject.SetActive(false);
        questionTMP.gameObject.SetActive(false);
        list1TMP.gameObject.SetActive(false);
        list2TMP.gameObject.SetActive(false);
        resultsTMP.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        startButton.onClick.AddListener(OnStartButtonPressed);
        doneButton.onClick.AddListener(OnDoneButtonPressed);
        nextButton.onClick.AddListener(OnNextButtonPressed);

    }

    void OnStartButtonPressed()
    {
        startButton.gameObject.SetActive(false);
        EnableQuestionUI();
        UpdateQuestionUI();
    }

    void OnDoneButtonPressed()
    {
        DisableQuestionUI();
        string resultText;
        if (CheckAnswers(out resultText))
        {
            resultsTMP.text = "Correct!";
        }
        else
        {
            resultsTMP.text = resultText;
        }
        resultsTMP.gameObject.SetActive(true);
        doneButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    void OnNextButtonPressed()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            resultsTMP.gameObject.SetActive(false);
            EnableQuestionUI();
            UpdateQuestionUI();
        }
        else
        {
            
            resultsTMP.text = "Quiz Completed!";
            
            nextButton.gameObject.SetActive(false);
        }
    }

    void EnableQuestionUI()
    {
        list1.SetActive(true);
        list2.SetActive(true);
        questionNumberTMP.gameObject.SetActive(true);
        questionTMP.gameObject.SetActive(true);
        list1TMP.gameObject.SetActive(true);
        list2TMP.gameObject.SetActive(true);
        doneButton.gameObject.SetActive(true);
    }

    void DisableQuestionUI()
    {
        list1.SetActive(false);
        list2.SetActive(false);
        questionNumberTMP.gameObject.SetActive(false);
        questionTMP.gameObject.SetActive(false);
        list1TMP.gameObject.SetActive(false);
        list2TMP.gameObject.SetActive(false);
    }

    void UpdateQuestionUI()
    {
        Question currentQuestion = questions[currentQuestionIndex];
        questionNumberTMP.text = currentQuestion.questionNumber;
        questionTMP.text = currentQuestion.questionText;
        list1TMP.text = currentQuestion.list1Text;
        list2TMP.text = currentQuestion.list2Text;
    }

    bool CheckAnswers(out string resultText)
    {
        resultText = "";
        int correctCount = 0;
        for (int i = 0; i < planetSlots.Count; i++)
        {
            GameObject planet = planetSlots[i].planet;
            GameObject correctSlot = planetSlots[i].correctSlots[currentQuestionIndex];
            if (planet.GetComponent<Collider>().bounds.Intersects(correctSlot.GetComponent<Collider>().bounds))
            {
                correctCount++;
            }
        }

        resultText = $"{correctCount}/{planetSlots.Count} correct\n";

        switch (currentQuestionIndex)
        {
            case 0:
                resultText += result1;
                break;
            case 1:
                resultText += result2;
                break;
            default:
                resultText += "Unknown question result";
                break;
        }

        return correctCount == planetSlots.Count;
    }

    bool CheckAllQuestionsCorrect()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            currentQuestionIndex = i;
            string resultText;
            if (!CheckAnswers(out resultText))
            {
                return false;
            }
        }
        return true;
    }
}
