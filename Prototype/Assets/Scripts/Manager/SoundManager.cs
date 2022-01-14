using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using UnityEngine.SceneManagement;
public class SoundManager : Singleton<SoundManager>
{
    private static AudioClip currentBgmClip;
    private static float bgmVolumStatic;
    public AudioSource bgmSource;
    public AudioClip defaultBGM;
    public float bgmVolume = 0.5f;
    protected override void Awake()
    {
        currentBgmClip = defaultBGM;
        bgmVolumStatic = bgmVolume;
        base.Awake();
        PlayBGM(currentBgmClip, bgmVolumStatic);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 || level == 0)
        {
            Destroy(gameObject);
        }
        PlayBGM(currentBgmClip, bgmVolumStatic);
    }
    public void PlayBGM(AudioClip bgmClip, float volume = 0.5f)
    {
       
        if (!bgmSource.isPlaying)
         {
                bgmSource.clip = bgmClip;
                bgmSource.volume = volume;
                bgmSource.Play();
        }
        
        if (bgmSource.clip.name != bgmClip.name)
        {

            bgmSource.Stop();
            bgmSource.clip = bgmClip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1, bool affectedBySlowmo = false)
    {
        var go = new GameObject(sfxClip.name);
        var audio = go.AddComponent<AudioSource>();
        if (affectedBySlowmo)
        {

            Timeline time = go.AddComponent<Timeline>();
            time.mode = TimelineMode.Global;
            time.globalClockKey = "Enemies";
        }
        
        audio.volume = volume;
        audio.clip = sfxClip;
        audio.Play();
        Destroy(go, sfxClip.length);
    }
}
