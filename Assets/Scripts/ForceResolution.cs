using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceResolution : MonoBehaviour
{

    private float lastWidth;
    private float lastHeight;


    void FixedUpdate() {
        if (Application.platform != RuntimePlatform.Android) {
            if (lastWidth != Screen.width) {
                Screen.SetResolution(Screen.width, Mathf.RoundToInt(Screen.width * (16f / 9f)), false);
            }
            else if (lastHeight != Screen.height) {
                Screen.SetResolution(Mathf.RoundToInt(Screen.height * (9f / 16f)), Screen.height, false);
            }

            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }
}
