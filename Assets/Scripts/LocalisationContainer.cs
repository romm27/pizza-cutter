using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Container", menuName = "Container/Create New Localisation Container")]
public class LocalisationContainer : ScriptableObject
{
    [Header("Data")]
    public GenericWords[] content;
}
