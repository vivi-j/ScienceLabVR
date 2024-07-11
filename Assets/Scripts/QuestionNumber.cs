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

    // Start is called before the first frame update
    void Start()
    {
        list1.SetActive(false);
        list2.SetActive(false);
        // Ensure all relevant UI elements are disabled at the start
        questionNumberTMP.gameObject.SetActive(false);
        questionTMP.gameObject.SetActive(false);
        list1TMP.gameObject.SetActive(false);
        list2TMP.gameObject.SetActive(false);
        resultsTMP.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        // Add listeners to the buttons
        startButton.onClick.AddListener(OnStartButtonPressed);
        doneButton.onClick.AddListener(OnDoneButtonPressed);
        nextButton.onClick.AddListener(OnNextButtonPressed);
    }

    // Method to be called when the Start button is pressed
    void OnStartButtonPressed()
    {
        startButton.gameObject.SetActive(false);
        EnableQuestionUI();
        UpdateQuestionUI();
        //list1TMP.gameObject.SetActive(true);
        //list2TMP.gameObject.SetActive(true);
    }

    // Method to be called when the Done button is pressed
    void OnDoneButtonPressed()
    {
        DisableQuestionUI();
        string resultText;
        if (CheckAnswers(out resultText))
        {
            resultsTMP.text = "Good job!";
        }
        else
        {
            resultsTMP.text = resultText;
        }
        resultsTMP.gameObject.SetActive(true);
        doneButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    // Method to be called when the Next button is pressed
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
            // Quiz finished
            resultsTMP.text = "Quiz Completed!";
            nextButton.gameObject.SetActive(false);
        }
    }

    // Enable question-related UI elements
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

    // Disable question-related UI elements
    void DisableQuestionUI()
    {
        list1.SetActive(false);
        list2.SetActive(false);
        questionNumberTMP.gameObject.SetActive(false);
        questionTMP.gameObject.SetActive(false);
        list1TMP.gameObject.SetActive(false);
        list2TMP.gameObject.SetActive(false);
    }

    // Update the text for the current question
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
        bool allCorrect = true;
        for (int i = 0; i < planetSlots.Count; i++)
        {
            GameObject planet = planetSlots[i].planet;
            GameObject correctSlot = planetSlots[i].correctSlots[currentQuestionIndex];
            if (!planet.GetComponent<Collider>().bounds.Intersects(correctSlot.GetComponent<Collider>().bounds))
            {
                allCorrect = false;
                resultText += $"{planet.name} should be in {correctSlot.name}\n";
            }
        }
        return allCorrect;
    }
}
