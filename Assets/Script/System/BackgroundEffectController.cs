using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��� �ܻ� ȿ���� ��Ʈ�� �ϱ����� ��ü
public class BackgroundEffectController : Singleton<BackgroundEffectController>
{
    public GameObject afterimage = null;//�ܻ�ȿ���� ����� ��ü

    //��� �ܻ�ȿ���� �ٲٴ� ����ġ �Լ�
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
