using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LoadingManager loadingManager;


    private void Start() {
        if(Application.platform == RuntimePlatform.Android) {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            loadingManager.LoadScene(2);
        }
    }
}
