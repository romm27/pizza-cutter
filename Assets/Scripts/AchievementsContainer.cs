using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "Container/Create New Achievement Container")]
public class AchievementsContainer : SOVariable
{
    public Achievement[] achievements;
}
