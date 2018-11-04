using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioSource audioSource;
    public AudioClip bgm;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.clip = bgm;
        StartCoroutine(FadeIn(audioSource, 1f));
    }

    public static IEnumerator FadeOut(AudioSource audioSource,float fadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume>0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while(audioSource.volume<1.0f) {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void BackgroundMusic(AudioClip bgm) {
        audioSource.clip = bgm;
        audioSource.Play();
        StartCoroutine(FadeIn(audioSource, .5f));
    }

    public void RobustBackgroundMusic(AudioClip bgm,AudioSource audioS) {
        audioSource.clip = bgm;
        audioSource.Play();
        StartCoroutine(FadeIn(audioS, .5f));
    }

    public void SoundFX(AudioClip sfx) {
        audioSource.PlayOneShot(sfx);
    }

    public void SoundSetting(bool cond) {
        if(cond)
            {
                audioSource.volume = 1;
            } else
                {
                    audioSource.volume = 0;
                }
    }
}
