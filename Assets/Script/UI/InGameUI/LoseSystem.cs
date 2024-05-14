using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSystem : MonoBehaviour
{
    public GameObject loseWindow;//승리 표시창

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.Lose, Lose);//TurnStart 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.Lose, Lose);//TurnStart 이벤트 제거
    }

    public void Lose()
    {
        loseWindow.SetActive(true);
    }
}
