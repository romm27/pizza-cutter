using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Manager", menuName = "Manager/Create New Localisation Manager")]
public class LocalisationManager : ScriptableObject
{
    [Header("References")]
    private Localise[] localises;
    public TextAsset localisationCSV;
    
    [Header("Settings")]
    public static string currentLang = "en_us";

    [Header("Data")]
    public UnityEvent OnLanguageChange;
    public LocalisationContainer genericWords;

    public static LocalisationManager instance;

    public static Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();


    public string CurrentLanguage {
        set {
            currentLang = value;
            OnLanguageChange.Invoke();
        }
        get {
            return currentLang;
        }
    }

    public void Awake() {
        FillDictionary();
    }

    public void Start() {
        instance = this;
        //TranslateScene();
    }


    //Methods
    private void FillLocalise() {
        localises = FindObjectsOfType<Localise>();
    }

    private void FillDictionary() {
        if (dic.Count == 0) {
            string[] lines = localisationCSV.text.Split('\n');

            string[] languages = lines[0].Split(',');
            foreach (string lang in languages) {
                string trimmedLang = lang.Trim();
                if (trimmedLang == "key") {
                    continue;
                }

                dic.Add(trimmedLang.Trim(), new Dictionary<string, string>());
                //Debug.Log(trimmedLang + "/" + currentLang);
            }

            for (int i = 1; i < lines.Length; i++) {
                string[] cells = lines[i].Split(',');
                for (int j = 1; j < cells.Length; j++) {
                    string language = languages[j].Trim();
                    string key = cells[0].Trim();
                    string value = cells[j].Trim();

                    value = value.Replace('"'.ToString(), "");
                    value = value.Replace("/n", '\n'.ToString());
                    //Debug.Log(language + "/" + key + "/" + value);
                    if (!dic.ContainsKey(key)) {
                        dic[language].Add(key, value);
                    }
                }
            }
        }
    }

    public void TranslateScene() {
        if(dic.Count == 0) {
            FillDictionary();
        }

        FillLocalise();
        foreach (Localise localise in localises) {
            if (localise.fixedTranslation) {
                if (dic.ContainsKey(currentLang)) {
                    if (dic[currentLang].ContainsKey(localise.translationKey)) {
                        localise.text = dic[currentLang][localise.translationKey];
                    }
                    else {
                        Debug.LogWarning(localise.translationKey + " is not a valid key!");
                    }
                }
                else {
                    Debug.LogError(currentLang + " is not a valid language!");
                }
            }
        }
    }


    //One timers
    public string GetTranslation(string key) {
        if (dic.Count == 0) {
            FillDictionary();
        }

        //Debug.Log(currentLang);
        if (dic[currentLang].ContainsKey(key)) {
            return dic[currentLang][key];
        }
        else {
            Debug.LogWarning(key + " is not a valid key!");
            return "INVALID_KEY";
        }
    }

    public string GetTranslation(string key, string language) {
        if (dic.ContainsKey(language)) {
            if (dic[language].ContainsKey(key)) {
                return dic[language][key];
            }
            else {
                Debug.LogWarning(key + " is not a valid key!");
                return "INVALID_KEY";
            }
        }
        else {
            Debug.LogWarning(language + " is not a valid language!");
            return "INVALID_LANG";
        }
    }

}

public struct GenericWords
{
    public string genericKey;
    public string currentReplaceWord;
}
