using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource, effectsSource, randomEffectsSource;

    [Range(0.1f, 0.5f)]
    [SerializeField]
    float volumeChangeMultiplier = 0.2f;

    [Range(0.1f, 0.5f)]
    [SerializeField]
    float pitchChangeMultiplier = 0.2f;

    float randEffectMaxVolume;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        effectsSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
        randEffectMaxVolume = randomEffectsSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    
    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }

    public void PlaySoundWithRandomValues(AudioClip clip)
    {
        randomEffectsSource.volume = Random.Range(randEffectMaxVolume - volumeChangeMultiplier, randEffectMaxVolume);
        randomEffectsSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        randomEffectsSource.PlayOneShot(clip);     
    }

    public void PlaySound(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }

    public void PlaySoundWithRandomValues(AudioClip clip, AudioSource source)
    {
        source.volume = Random.Range(randEffectMaxVolume - volumeChangeMultiplier, randEffectMaxVolume);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleEffects()
    {
        effectsSource.mute = !effectsSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void EffectsVolume(float volume)
    {
        randomEffectsSource.volume = volume;
        randEffectMaxVolume = volume;
        effectsSource.volume = volume;
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }
}
