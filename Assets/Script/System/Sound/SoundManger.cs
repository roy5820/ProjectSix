using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : Singleton<SoundManger>
{
    // BGM�� SFX�� ���� AudioSources
    public AudioSource bgmSource;
    public AudioSource sfxSource;


    // BGM ���
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    // SFX ���
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}
