using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Manager", menuName = "Manager/Create New Loading Manager")]
public class LoadingManager : ScriptableObject
{
    [Header("Settings")]
    public int loadingScene = 1;
    public int calledScene;

    //Methods
    public void LoadScene(int scene) {
        calledScene = scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadingScene);
    }
}
