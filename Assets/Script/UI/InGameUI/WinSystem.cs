using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    public GameObject winWindow;//�¸� ǥ��â

    //Ȱ��ȭ�� �̺�Ʈ ����
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.Win, Win);//TurnStart �̺�Ʈ ����
    }

    //��Ȱ��ȭ�� �̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.Lose, Win);//TurnStart �̺�Ʈ ����
    }

    public void Win()
    {
        winWindow.SetActive(true);
    }
}
