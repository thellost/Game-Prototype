using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : Singleton<SoundManager>
{
    private static AudioClip currentBgmClip;
    public AudioSource bgmSource;
    public AudioClip defaultBGM;

    protected override void Awake()
    {
        currentBgmClip = defaultBGM;
        base.Awake();
        PlayBGM(currentBgmClip);
    }
    private void OnLevelWasLoaded(int level)
    {
        PlayBGM(currentBgmClip);
    }
    public void PlayBGM(AudioClip bgmClip)
    {
       
        if (!bgmSource.isPlaying)
         {
                Debug.Log("S");
                bgmSource.clip = bgmClip;
                bgmSource.volume = 0.5f;
                bgmSource.Play();
        }
        if (bgmSource.clip.name != bgmClip.name)
        {

            bgmSource.Stop();
            bgmSource.clip = bgmClip;
            bgmSource.volume = 0.5f;
            bgmSource.Play();
        }
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
