using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "Container/Create New Singleton Container")]
public class SingletonReference : SOVariable
{

    public BackgroundMusicManager singleton;

    //Methods
    public void CheckConditionsForAchievement(int index) {
        AchievementManager.instance.CheckConditionsForAchievementIndex(index);
    }
}
