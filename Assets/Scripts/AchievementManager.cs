using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Manager", menuName = "Manager/Create New Achievement Manager")]
public class AchievementManager : ScriptableObject
{
    [Header("References")]
    [SerializeField] GameObjectContainer achievementDisplayContainer;

    [Header("Data Reference")]
    public AchievementsContainer achievementContainer;
    public GameObjectContainer gameManagerContainer;

    [Header("Achievement Display Settings")]
    public float outOfScreenX = 660;
    public float InScreenX = 420;
    public float displayMoveSpeed = 4f;
    public float persistentTime = 4f;

    private GotAchievementWindow achievementDisplay;


    public static AchievementManager instance;

    private void Awake() {
        instance = this;
    }


    //Methods
    //Called from Unity Actions
    public void Initialise() {
        achievementDisplay = achievementDisplayContainer.content.GetComponent<GotAchievementWindow>();
        for (int i = 0; i < achievementContainer.achievements.Length; i++) {
            achievementContainer.achievements[i].UpdateReferences();
        }
        LoadAchievements();
    }

    public void CheckConditionsForAchievementIndex(int index) {
        if (!achievementContainer.achievements[index].achieved && achievementContainer.achievements[index].CheckCompletion()) {
            achievementContainer.achievements[index].achieved = true;
            OnAchieved(index);
            SaveAchievements();
        }
        else {
            //Debug.Log(achievementContainer.achievements[index].achievementName + " " + achievementContainer.achievements[index].achieved);
        }
    }

    public void OnAchieved(int index) {
        if(achievementDisplay == null) {
            achievementDisplay = achievementDisplayContainer.content.GetComponent<GotAchievementWindow>();
        }
        achievementDisplay.achievementNameDisplay.text = achievementContainer.achievements[index].achievementName;
        achievementDisplay.animator.Play("show");
    }

    public void SaveAchievements() {
        for(int i = 0; i < achievementContainer.achievements.Length; i++) {
            int state = achievementContainer.achievements[i].achieved ? 1 : 0;
            PlayerPrefs.SetInt(achievementContainer.achievements[i].saveKey, state);
        }
        //Debug.Log("saved");
        PlayerPrefs.Save();
    }

    public void LoadAchievements() {
        //Debug.Log("loaded");
        for (int i = 0; i < achievementContainer.achievements.Length; i++) {
            string key = achievementContainer.achievements[i].saveKey;
            if (PlayerPrefs.HasKey(key)) {
                achievementContainer.achievements[i].achieved = PlayerPrefs.GetInt(key) > 0;
            }
            else {
                achievementContainer.achievements[i].achieved = false;
            }
        }
    }

    public void ResetAllAchievements() {
        for (int i = 0; i < achievementContainer.achievements.Length; i++) {
            PlayerPrefs.SetInt(achievementContainer.achievements[i].saveKey, 0);
        }
        PlayerPrefs.Save();

        LoadAchievements();
    }
}
