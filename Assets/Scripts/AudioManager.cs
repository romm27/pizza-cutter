using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource oneShotAudioPrefab;

    [Header("Settings")]
    public float globalVolumeMultiplier = 0.5f;


    public static AudioManager instance;

    private void Awake() {
        instance = this;
    }

    //Methods
    public void PlayClip(AudioClip audioClip, float volume) {
        AudioSource temp = Instantiate(oneShotAudioPrefab, transform);
        temp.clip = audioClip;
        temp.volume = volume * globalVolumeMultiplier;
        temp.Play();
        Destroy(temp.gameObject, audioClip.length);
    }
}
