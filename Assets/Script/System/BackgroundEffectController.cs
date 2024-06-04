using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//배경 잔상 효과를 컨트롤 하기위한 객체
public class BackgroundEffectController : Singleton<BackgroundEffectController>
{
    public GameObject afterimage = null;//잔상효과가 적용된 객체

    //배경 잔상효과를 바꾸는 스위치 함수
    public void OnSwitchBackgroundAfterimage()
    {
        if (afterimage)
        {
            if (afterimage.activeSelf)
                afterimage.SetActive(false);
            else
                afterimage.SetActive(true);
        }
    }
}
