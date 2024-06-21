using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Degree Question", menuName = "Question/Create New Degree Question")]
public class QuestionDegree : Question
{
    public enum DegreeQuestionType { Begginer, Easy, Odd}//Begginner - 45°, 90°..., Easy - Whole numbers, Odd - random
    [Header("Type")]
    public DegreeQuestionType questionType = DegreeQuestionType.Begginer;

    [Header("Question Structures Structure")]
    [TextArea(20, 10)] public string[] structures;

    public override GeneratedQuestion GenerateQuestion() {
        GeneratedQuestion temp = new GeneratedQuestion();

        switch (questionType) {
            case DegreeQuestionType.Begginer:
                temp.answer = Random.Range(1, 8 + 1) * 45;
                break;
            case DegreeQuestionType.Easy:
                temp.answer = Random.Range(1, 72 + 1) * 5;
                break;
            case DegreeQuestionType.Odd:
                temp.answer = Random.Range(0,360);
                break;
        }
        temp.question = structures[Random.Range(0, structures.Length)].Replace("$", temp.answer.ToString());
        temp.extraTime = extraTimeForQuestion;
        return temp;
    }

}
