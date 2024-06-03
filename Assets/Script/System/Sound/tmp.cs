using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class tmp : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        //���� �� �� �ʱ�ȭ
        float bgmValue;
        audioMixer.GetFloat("BGM", out bgmValue);
        bgmSlider.value = bgmValue;
        float sfxValue;
        audioMixer.GetFloat("SFX", out sfxValue);
        sfxSlider.value = sfxValue;
    }

    //BGM ��Ʈ��
    public void BGMControl()
    {
        float sound = bgmSlider.value;

        if (sound == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", sound);
    }

    //SFX ��Ʈ��
    public void SFXControl()
    {
        float sound = sfxSlider.value;

        if (sound == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", sound);
    }
}
