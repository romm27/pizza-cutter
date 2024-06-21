using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rotator wheel;
    [SerializeField] QuestionManager questionManager;
    [SerializeField] PizzaEjector pizzaGhost;
    [SerializeField] NewPizza newPizza;
    [SerializeField] Image circle;
    [SerializeField] Image pizzaBackground;
    [SerializeField] Image ejectorSprite;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] TextMeshProUGUI currentQuestionDisplay;
    [SerializeField] TextMeshProUGUI questionNumberDisplay;
    [SerializeField] TextMeshProUGUI currentScoreDisplay;
    [SerializeField] GameObject gameOverObject;
    [SerializeField] GameObject correct;
    [SerializeField] GameObject incorrect;
    [SerializeField] AudioSource countDown;
    [SerializeField] GameObjectContainer container;

    [Header("Textures")]
    [SerializeField] Sprite[] pizzas;


    [Header("Helper")]
    public float angleTolerance = 10f;
    public float baseScore;
    public AnimationCurve scoreGainPerAngleCloseness;

    [Header("Game Settings")]
    public float timerGoesRedAt = 5f;
    public float gameOverTime = 4f;
    public float timeToNextQuestion = 5f;
    public Color defaultCircleColor = Color.white;

    [Header("Game State")]
    public int currentQuestion;
    public int currentScore;
    public float timer;
    public GeneratedQuestion currentGeneratedQuestion;
    public Queue<GeneratedQuestion> questionQueue = new Queue<GeneratedQuestion>();

    [Header("Statistics")]
    public float playTime = 0f;
    public int lastAswnerScore;

    [Header("Phases")]
    public Phase[] phases;
    public Phase infinitePhase;

    [Header("Audio Gambiarra")]
    //public AudioClip right;
    public AudioClip rightMid;
    public AudioClip rightFull;
    public AudioClip wrong;

    [Header("Actions")]
     public UnityEvent OnCorrectAnswer;
     public UnityEvent OnIncorrectAswer;
     public UnityEvent OnReset;


    [HideInInspector] public List<float> accurracy = new List<float>();

    public bool Waiting {
        get {
            return !correct.activeInHierarchy && !incorrect.activeInHierarchy && !gameOverObject.activeInHierarchy;
        }
    }

    public Phase CurrentPhase {
        get {
            int i = 0;
            foreach (Phase phase in phases) {
                i += phase.duration;
                if (i > currentQuestion) {
                    return phase;
                }
            }
            Debug.LogError("No Valid Phase found at " + i);
            return new Phase();
        }
    }

    private void Awake() {
        container.content = this.gameObject;
    }

    private void Start() {
        timer = currentGeneratedQuestion.extraTime + CurrentPhase.timer;
        if (questionQueue.Count == 0) {
            questionQueue = new Queue<GeneratedQuestion>(questionManager.GenerateQuestionsFoPhase(CurrentPhase.id));
        }
        DisplayQuestion(questionQueue.Dequeue());
        defaultCircleColor = wheel.circle.color;

        //Reset
        playTime = 0;
        accurracy = new List<float>();
    }

    public void Update() {
        ManageTimer();
    }

    public void OnValidate() {
        for(int i = 0; i < phases.Length; i++) {
            phases[i].id = i;
        }
    }

    //Methods
    public void ManageTimer() {
        if(Waiting) {
            playTime += Time.deltaTime;
            timer -= Time.deltaTime;
            timerDisplay.text = Mathf.RoundToInt(timer).ToString();

            if(timer < timerGoesRedAt) {
                timerDisplay.color = Color.red;
                if (!countDown.isPlaying) {
                    countDown.Play();
                }
            }
            else {
                timerDisplay.color = Color.white;
                if (countDown.isPlaying) {
                    countDown.Stop();
                }
            }

            if (timer < 0) {
                ButtonNextQuestion();
            }
        }
    }


    public void ButtonNextQuestion() {
        if (Waiting) {
            NextQuestion();
        }
    }

    public void NextQuestion() {
        if (AngleInRange()) {
            StartCoroutine(Correct());
        }
        else {
            StartCoroutine(Incorrect());
        }
    }

    private bool AngleInRange() {
        return wheel.Angle < currentGeneratedQuestion.answer + angleTolerance && wheel.Angle > currentGeneratedQuestion.answer - angleTolerance;
    }

    public void DisplayQuestion(GeneratedQuestion question){
        currentGeneratedQuestion = question;
        currentQuestionDisplay.text = question.question;
    }

    public IEnumerator Correct() {
        int pizzaSpriteIndex = Random.Range(0, pizzas.Length);
        newPizza.GenerateNewPizza(pizzas[pizzaSpriteIndex]);
        countDown.Stop();
        correct.SetActive(true);
        pizzaGhost.Initialise(wheel.Angle);

        float evaluator = currentGeneratedQuestion.answer > wheel.Angle ? (currentGeneratedQuestion.answer - wheel.Angle) : (wheel.Angle - currentGeneratedQuestion.answer);
        evaluator /= angleTolerance;

        int value = Mathf.FloorToInt(baseScore * scoreGainPerAngleCloseness.Evaluate(evaluator));
        accurracy.Add(value / baseScore);
        //Debug.Log(evaluator);
        currentQuestionDisplay.text = "+" + value;
        currentQuestionDisplay.color = Color.green;
        currentScore += value;
        lastAswnerScore = value;

        //Audio
        if(value == baseScore) {
            AudioManager.instance.PlayClip(rightFull, 0.9f);
        }
        else {
            AudioManager.instance.PlayClip(rightMid, 0.9f);
        }

        if (OnCorrectAnswer != null) {
            OnCorrectAnswer.Invoke();
        }

        pizzaBackground.fillAmount = 0;

        yield return new WaitForSeconds(timeToNextQuestion);
        pizzaBackground.sprite = pizzas[pizzaSpriteIndex];
        pizzaBackground.fillAmount = 1;
        circle.sprite = pizzas[pizzaSpriteIndex];
        ejectorSprite.sprite = pizzas[pizzaSpriteIndex];

        wheel.Angle = 359;
        wheel.UpdateCircle();
        wheel.circle.color = defaultCircleColor;
        correct.SetActive(false);

        //Next
        if (questionQueue.Count == 0) {
            questionQueue = new Queue<GeneratedQuestion>(questionManager.GenerateQuestionsFoPhase(CurrentPhase.id));
        }

        DisplayQuestion(questionQueue.Dequeue());
        currentQuestion++;

        currentScoreDisplay.text = currentScore.ToString();
        questionNumberDisplay.text = currentQuestion.ToString();
        currentQuestionDisplay.color = Color.white;
        timer = currentGeneratedQuestion.extraTime + CurrentPhase.timer;

        OnReset.Invoke();
    }

    public IEnumerator Incorrect() {
        if (OnIncorrectAswer != null) {
            OnIncorrectAswer.Invoke();
        }
        countDown.Stop();
        AudioManager.instance.PlayClip(wrong, 4f);
        incorrect.SetActive(true);
        wheel.Angle = currentGeneratedQuestion.answer;
        wheel.UpdateCircle();
        yield return new WaitForSeconds(gameOverTime);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        gameOverObject.SetActive(true);
    }

    public float GetTimeForQuestion() {
        return CurrentPhase.timer + currentGeneratedQuestion.extraTime;
    }
}

[System.Serializable]
public struct Phase
{
    public int id;
    public int duration;

    [Header("Settings")]
    public float timer;
}

