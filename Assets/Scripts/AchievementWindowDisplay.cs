using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementWindowDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image icon;
    public TextMeshProUGUI achievementNameTMPro;
    public TextMeshProUGUI achievementdescriptionTMPro;
    public ParticleSystem effect;

    //Methods
    public void InjectData(string _name, string _achievements, bool _completed) {
        achievementNameTMPro.text = _name;
        achievementdescriptionTMPro.text = _achievements;

        icon.color = _completed ? Color.yellow : Color.black;

        effect.gameObject.SetActive(_completed);
        effect.Play();
    }

}
