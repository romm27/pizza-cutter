using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Localise : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI target;

    [Header("Settings")]
    public bool fixedTranslation = true;
    public string translationKey = "";

    public string text {
        get {
            return target.text;
        }
        set {
            target.text = value;
        }
    }

    public void OnEnabled() {
        Translate();
    }

    private void OnValidate() {
        target = transform.GetComponent<TextMeshProUGUI>();
    }

    //Methods
    public void Translate() {
        Debug.Log(LocalisationManager.instance);
        text = LocalisationManager.instance.GetTranslation(translationKey);
    }
}
