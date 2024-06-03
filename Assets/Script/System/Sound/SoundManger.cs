using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : Singleton<SoundManger>
{
    // BGM과 SFX를 위한 AudioSources
    public AudioSource bgmSource;
    public AudioSource sfxSource;


    // BGM 재생
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    // SFX 재생
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}
