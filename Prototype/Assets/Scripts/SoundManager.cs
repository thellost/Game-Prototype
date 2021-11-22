using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip defaultBGM;

    private void Start()
    {
        PlayBGM(defaultBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip sfxclip)
    {
        sfxSource.clip = sfxclip;
        sfxSource.Play();
    }

}
