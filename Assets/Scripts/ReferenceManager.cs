using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    [Header("Matches")]
    public Match[] matches;

    public void Awake() {
        FillReferences();
    }

    //Methods
    public void FillReferences() {
        for (int i = 0; i < matches.Length; i++) {
            matches[i].target.content = matches[i].reference;
        }
    }

}


[System.Serializable]
public struct Match
{
    [Header("Match")]
    public string commentary;
    public GameObject reference;
    public GameObjectContainer target;
}