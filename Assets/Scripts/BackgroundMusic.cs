using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    AudioClip _backgroundAudio;
    AudioSource _audioSource;

    private static BackgroundMusic instance = null;
    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        _audioSource = GetComponent<AudioSource>();

        StartAudio();
    }

    void StartAudio()
    {
        _audioSource.clip = _backgroundAudio;
        _audioSource.volume = 0.5f;
        _audioSource.Play();
    }
   
}
