using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmSource;
    public AudioClip defaultBGM;

    private void Start()
    {
        PlayBGM(defaultBGM);
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.volume = 0.04f;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1)
    {
        var go = new GameObject(sfxClip.name);
        var audio = go.AddComponent<AudioSource>();

        audio.volume = volume;
        audio.clip = sfxClip;
        audio.Play();
        Destroy(go, sfxClip.length);
    }
}
