using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Question : ScriptableObject
{
    [Header("Spawn Conditions")]
    public int weight = 1;
    public int introductionPhase = 0;
    public int[] limitedPhaseTriger; //leave empty for all

    [Header("Extra")]
    public float extraTimeForQuestion = 10f;


    //Methods
    public virtual GeneratedQuestion GenerateQuestion() {
        return new GeneratedQuestion();
    }
}

[System.Serializable]
public struct GeneratedQuestion{
    public string question;
    public float answer;
    public float extraTime;

    public GeneratedQuestion(string _question, float _answer, float _extraTime) {
        question = _question;
        answer = _answer;
        extraTime = _extraTime;
    }
}
