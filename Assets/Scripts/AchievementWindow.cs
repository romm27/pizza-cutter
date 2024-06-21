using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementWindow : MonoBehaviour
{
    [Header("References")]
    public AchievementWindowDisplay achievementDisplayPrefab;
    public SingletonReference singleton;
    public Transform achievementDisplaySpawnTransform;
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] LocalisationManager localisationManager;

    private AchievementWindowDisplay[] achievementWindowDisplays;




    private void OnEnable() {
        GenerateGFX();
    }

    //Methods
    public void GenerateGFX() {
        if (achievementWindowDisplays == null || achievementWindowDisplays.Length == 0) {
            List<AchievementWindowDisplay> displays = new List<AchievementWindowDisplay>();
            for (int i = 0; i < achievementManager.achievementContainer.achievements.Length; i++) {
                AchievementWindowDisplay temp = Instantiate(achievementDisplayPrefab, achievementDisplaySpawnTransform);
                displays.Add(temp);
            }
            achievementWindowDisplays = displays.ToArray();
        }

        for (int i = 0; i < achievementWindowDisplays.Length; i++) {
            string name = localisationManager.GetTranslation(achievementManager.achievementContainer.achievements[i].saveKey);
            string desc = localisationManager.GetTranslation(achievementManager.achievementContainer.achievements[i].saveKey + Achievement.descExtra);
            achievementWindowDisplays[i].InjectData(name, desc, achievementManager.achievementContainer.achievements[i].achieved);
        }
    }
}
