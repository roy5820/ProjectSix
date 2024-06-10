using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayerController : MonoBehaviour
{
    public AudioClip bgmClip;// Àç»ýÇÒ BGM
    void Start()
    {
        if (bgmClip != null)
            SoundManger.Instance.PlayBGM(bgmClip);
    }
}
