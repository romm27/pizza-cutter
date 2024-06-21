using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject newHighscore;
    [SerializeField] TextMeshProUGUI endText;
    [SerializeField] SingletonReference singletonReference;
    [SerializeField] LocalisationManager localisationManager;
    [SerializeField] LoadingManager loadingManager;


    private void OnEnable() {
        bool record = gameManager.currentScore > PlayerPrefs.GetInt("recordScore");
        if (record) {
            PlayerPrefs.SetInt("recordScore", gameManager.currentScore);
        }
        PlayerPrefs.Save();

        newHighscore.SetActive(record);

        string questions = (gameManager.currentQuestion).ToString();
        string score = gameManager.currentScore.ToString();
        string accuracy = GetAccuracy() + "%";
        string playtime = System.Math.Round(gameManager.playTime, 2) + "s";
        string avgresponsetime = GetResponseTime() + "s";

        string text = localisationManager.GetTranslation("game_over_text");
        localisationManager.TranslateScene();

        text = text.Replace("{questions}", questions);
        text = text.Replace("{score}", score);
        text = text.Replace("{accuracy}", accuracy);
        text = text.Replace("{playtime}", playtime);
        text = text.Replace("{avgresponsetime}", avgresponsetime);

        endText.text = text;
    }

    //Methods
    public void TryAgain() {
        loadingManager.LoadScene(3);
    }

    public void MainMenu() {
        loadingManager.LoadScene(2);
    }

    public float GetAccuracy() {
        float some = 0;
        foreach(float acc in gameManager.accurracy) {
            some += acc;
        }

        if(some <= 0) {
            return 0;
        }
        return Mathf.RoundToInt(some / gameManager.accurracy.Count * 100f);
    }

    public float GetResponseTime() {
        if(gameManager.currentQuestion <= 0) {
            return (float)System.Math.Round(gameManager.playTime, 2);
        }
        return (float)System.Math.Round(gameManager.playTime / gameManager.currentQuestion, 2);
    }

}
