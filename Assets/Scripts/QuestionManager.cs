using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;

    [Header("Question Templates")]
    [SerializeField] Question[] questionTemplates;
    [SerializeField] Question[] infiniteQuestionTemplates;


    //Methods
    public List<GeneratedQuestion> GenerateQuestionsFoPhase(int phase) {
        List<GeneratedQuestion> generatedQuestions = new List<GeneratedQuestion>();
        List<Question> validTemplates = new List<Question>();

        Phase currentPhase = new Phase();
        if (phase < gameManager.phases.Length) {
            //Get valid templates
            currentPhase = gameManager.phases[phase];
            foreach (Question question in questionTemplates) {
                if (question.introductionPhase <= currentPhase.id) {
                    validTemplates.Add(question);
                }
            }
        }
        else {
            currentPhase = gameManager.infinitePhase;
            validTemplates = new List<Question>(infiniteQuestionTemplates);
        }
            

        //Generate Questions
        for (int i = 0; i < currentPhase.duration; i++) {
            int maxRandomizer = AddWeights(validTemplates.ToArray());
            int result = Random.Range(0, maxRandomizer + 1);
            generatedQuestions.Add(validTemplates[GetQuestionIdFromWeight(validTemplates.ToArray(), result)].GenerateQuestion());
        }
        return generatedQuestions;
    }

    public int AddWeights(Question[] questions) {
        int result = 0;
        foreach(Question question in questions) {
            result += question.weight;
        }
        return result;
    }

    public int GetQuestionIdFromWeight(Question[] questions, int randomResult) {
        int current = 0;
        for (int i = 0; i < questions.Length; i++) {
            current += questions[i].weight;
            if (randomResult <= current) {
                return i;
            }
        }
        Debug.LogError(randomResult + " is an invalid weight!");
        return -1;
    }
}
