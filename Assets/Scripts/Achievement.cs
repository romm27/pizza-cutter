using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : ScriptableObject
{
    [Header("Data")]
    public bool achieved = false;

    [Header("Achievement Settings")]
    public string saveKey;
    public string achievementName;
    [TextArea(20, 10)] public string description;
    public int[] availbleScenes;

    [Header("Localisation")]
    public string nameKey;


    public static string descExtra = "_desc";


    //Methods
    public virtual void UpdateReferences() {

    }
    public virtual bool CheckCompletion() {
        return false;
    }

}
