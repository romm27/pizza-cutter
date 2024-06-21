using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Add Question", menuName = "Question/Create New Add Question")]
public class QuestionAdd : Question
{
    public enum AddQuestionType { Easy, Medium, Hard }
    public AddQuestionType questionType = AddQuestionType.Easy;

    [Header("Settings")]
    public float piChance = 0.45f;
    public float mixedPiChance = 0.45f;

    [Header("Question Structures Structure")]
    [TextArea(20, 10)] public string[] structures;

    public override GeneratedQuestion GenerateQuestion() {
        GeneratedQuestion temp = new GeneratedQuestion();
        float a = 0;
        float b = 0;

        string aSymbol = "π";
        string bSymbol = "π";

        bool pi = questionType != AddQuestionType.Easy ? Random.Range(0, 1f) <= piChance : false;
        bool mixedPi = pi && questionType == AddQuestionType.Hard && Random.Range(0, 1f) <= mixedPiChance;

        switch (questionType) {
            case AddQuestionType.Easy:
                a = (int)Random.Range(0, 72 + 1) * 5f;
                b = (int)Random.Range(0, 72 + 1) * 5f;

                aSymbol = "°";
                bSymbol = "°";
                break;
            case AddQuestionType.Medium:
                if (pi) {
                    a = (int)Random.Range(0, 36 + 1) * 10f / 180;
                    b = (int)Random.Range(0, 36 + 1) * 10f / 180;
                }
                else {
                    a = (int)Random.Range(0, 359);
                    b = (int)Random.Range(0, 359);
                    aSymbol = "°";
                    bSymbol = "°";
                }
                break;
            case AddQuestionType.Hard:
                if (pi) {
                    if (!mixedPi) {
                        a = (int)Random.Range(0, 36 + 1) * 10f / 180;
                        b = (int)Random.Range(0, 36 + 1) * 10f / 180;
                    }
                    else{
                        a = (int)Random.Range(0, 36 + 1) * 10f / 180; //Pi
                        b = Random.Range(0, 359f);
                        //aSymbol = "";
                        bSymbol = "°";
                    }
                }
                else {
                    a = Random.Range(0, 359f);
                    b = Random.Range(0, 359f);
                    aSymbol = "°";
                    bSymbol = "°";
                }
                break;
        }
        if (!pi) {
            temp.answer = a + b;
        }
        else {
            if (!mixedPi) {
                temp.answer = 180f * (a + b);
            }
            else {
                temp.answer = (a * 180f) + b;
            }
        }

        if (temp.answer > 360f) {
            temp.answer -= 360f;
        }

        a = (float)System.Math.Round(a, 2);
        b = (float)System.Math.Round(b, 2);

        temp.question = structures[Random.Range(0, structures.Length)].Replace("$1", a.ToString() + aSymbol).Replace("$2", b.ToString() + bSymbol);
        temp.extraTime = extraTimeForQuestion;
        return temp;
    }
}
