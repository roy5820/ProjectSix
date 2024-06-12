using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSteelTImer : MonoBehaviour
{
    public GameObject rewardObj = null;//보상 표시 오브젝트
    public GameObject turnSteelTimerObj = null;
    public Image timerBar = null;
    public float timerTime = 10;//타이머 시간
    private Coroutine timerCoroutine = null;//타이머 구현 코루틴

    private void Start()
    {
        //보스 존재 유무에 따른 UI변화
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
        {
            if(rewardObj)
                rewardObj.SetActive(false);
            if (turnSteelTimerObj)
                turnSteelTimerObj.SetActive(true);
        }
    }

    //이벤트 등록
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, OnStartTimer);
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, OnStopTimer);
    }

    //이벤트 해제
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, OnStartTimer);
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, OnStopTimer);
    }

    //타이머 작동 함수
    private void OnStartTimer()
    {
        timerCoroutine = StartCoroutine(TurnSteelTimer(timerTime));
    }

    private void OnStopTimer()
    {
        if(timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }

    //타이머 구현 코루틴 함수
    private IEnumerator TurnSteelTimer(float timerTime)
    {
        float nowTimerTime = timerTime;
        while (nowTimerTime > 0)
        {
            nowTimerTime -= Time.deltaTime;
            nowTimerTime = Mathf.Clamp(nowTimerTime, 0, timerTime); // 최소값 0, 최대값 maxHealth로 제한
            //fillAmount설정
            timerBar.fillAmount = nowTimerTime / timerTime;

            yield return null;
        }
    }
}
