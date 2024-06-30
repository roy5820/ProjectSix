using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    public GameObject winWindow;//승리 표시창

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.Win, Win);//TurnStart 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.Lose, Win);//TurnStart 이벤트 제거
    }

    public void Win()
    {
        //보스 사망 컷씬 출력
        winWindow.SetActive(true);
    }

    IEnumerator WinCutScene()
    {

        yield return null;
    }
}
