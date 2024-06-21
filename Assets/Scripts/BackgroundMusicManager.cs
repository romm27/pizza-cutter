using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("References")]
    public AudioSource source;
    [SerializeField] SingletonReference reference;

    private void Awake() {
        if (reference.singleton == null || reference.singleton == this) {
            DontDestroyOnLoad(this.gameObject);
            reference.singleton = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

}
