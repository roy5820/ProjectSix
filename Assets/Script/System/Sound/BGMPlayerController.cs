using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayerController : MonoBehaviour
{
    public AudioClip bgmClip;// ����� BGM
    void Start()
    {
        if (bgmClip != null)
            SoundManger.Instance.PlayBGM(bgmClip);
    }
}
