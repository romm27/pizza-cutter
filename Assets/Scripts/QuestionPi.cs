using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pi Question", menuName = "Question/Create New Pi Question")]
public class QuestionPi : Question
{
    public enum PiQuestionType { Round, Fractal }//Begginner - 45°, 90°..., Easy - Whole numbers, Odd - random
    [Header("Type")]
    public PiQuestionType questionType = PiQuestionType.Round;

    [Header("Question Structures Structure")]
    [TextArea(20, 10)] public string[] structures;

    public override GeneratedQuestion GenerateQuestion() {
        GeneratedQuestion temp = new GeneratedQuestion();
        float pi = 0; 

        switch (questionType) {
            case PiQuestionType.Round:
                pi = (int)Random.Range(1, 2 + 1);
                break;
            case PiQuestionType.Fractal:
                pi = (int)Random.Range(0, 36 + 1) * 10f / 180;
                break;
        }
        temp.answer = pi * 180f;
        temp.question = structures[Random.Range(0, structures.Length)].Replace("$", System.Math.Round(pi, 2).ToString());
        temp.extraTime = extraTimeForQuestion;
        return temp;
    }
}
