using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] TextMeshProUGUI songButton;
    [SerializeField] GameObject achievementObject;
    [SerializeField] GameObject resetHighscoreObject;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] LocalisationManager localisationManager;
    [SerializeField] SingletonReference singletonReference;
    [SerializeField] LoadingManager loadingManager;

    [Header("Localisation")]
    public int currentLanguage = 0;
    public string[] supportedLanguages;

    [Header("Unity Events")]
    public UnityEvent OnResetWidowOpen;
    public UnityEvent OnCreditsWidowOpen;

    private AudioSource backgroundTheme;


    private void Start() {
        backgroundTheme = singletonReference.singleton.GetComponent<BackgroundMusicManager>().source;

        //Initialise Prefs
        if (!PlayerPrefs.HasKey("recordScore")) {
            PlayerPrefs.SetInt("recordScore", 0);
        }
        else if(PlayerPrefs.GetInt("recordScore") > 0){
            highscore.text = "High Score:" + PlayerPrefs.GetInt("recordScore");
        }

        if (!PlayerPrefs.HasKey("music")) {
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.Save();
            EnableSong(true);
        }
        else {//if(Time.realtimeSinceStartup < 3f) {
            EnableSong(PlayerPrefs.GetInt("music") > 0);
        }

        if (!PlayerPrefs.HasKey("language")) {
            PlayerPrefs.SetInt("language", 0);
        }
        else {
            currentLanguage = PlayerPrefs.GetInt("language");
        }

        TranslateMainMenu();
        achievementManager.Initialise();
    }

    //Methods
    public void StartTheGame() {
        loadingManager.LoadScene(3);
    }

    public void Exit() {
        Application.Quit();
    }

    public void SwitchSong() {
        if (backgroundTheme.isPlaying) {
            EnableSong(false);
            PlayerPrefs.SetInt("music", 0);
        }
        else {
            EnableSong(true);
            PlayerPrefs.SetInt("music", 1);
        }
        PlayerPrefs.Save();
    }

    public void ShowAchievements(bool value) {
        achievementObject.SetActive(value);
    }

    public void SwitchAchievements() {
        ShowAchievements(!achievementObject.activeInHierarchy);
    }

    public void EnableSong(bool value) {
        //Debug.Log(value);
        songButton.text = localisationManager.GetTranslation("background_music_button");
        if (value) {
            if (!backgroundTheme.isPlaying) {
                backgroundTheme.Play();
            }
            songButton.text += localisationManager.GetTranslation("on");
        }
        else {
            backgroundTheme.Stop();
            songButton.text += localisationManager.GetTranslation("off");
        }
    }

    public void ResetHighScore() {
        PlayerPrefs.SetInt("recordScore", 0);
        achievementManager.ResetAllAchievements();
        PlayerPrefs.Save();
        highscore.text = "";
    }

    public void SwitchHighscoreReset() {
        bool temp = resetHighscoreObject.activeInHierarchy;
        resetHighscoreObject.SetActive(!temp);
        if (resetHighscoreObject.activeInHierarchy) {
            OnResetWidowOpen.Invoke();
        }
    }

    public void SwitchCredits() {
        creditsScreen.SetActive(!creditsScreen.activeInHierarchy);
        if (creditsScreen.activeInHierarchy) {
            OnCreditsWidowOpen.Invoke();
        }
    }

    public void SwitchLanguage() {
        currentLanguage++;
        if(currentLanguage > supportedLanguages.Length - 1) {
            currentLanguage = 0;
        }
        PlayerPrefs.SetInt("language", currentLanguage);
        PlayerPrefs.Save();
        TranslateMainMenu();
    }

    public void TranslateMainMenu() {
        localisationManager.CurrentLanguage = supportedLanguages[currentLanguage];
        localisationManager.TranslateScene();
        EnableSong(PlayerPrefs.GetInt("music") > 0);
    }
}
