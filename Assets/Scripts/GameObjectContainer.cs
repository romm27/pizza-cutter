using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "Container/Create New Container")]
public class GameObjectContainer : SOVariable
{
    [Header("Data")]
    public GameObject content;
}
