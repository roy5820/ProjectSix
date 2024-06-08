using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSteelTImer : MonoBehaviour
{
    public GameObject rewardObj = null;//���� ǥ�� ������Ʈ
    public GameObject turnSteelTimerObj = null;
    public Image timerBar = null;
    public float timerTime = 10;//Ÿ�̸� �ð�
    private Coroutine timerCoroutine = null;//Ÿ�̸� ���� �ڷ�ƾ

    private void Start()
    {
        //���� ���� ������ ���� UI��ȭ
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
        {
            if(rewardObj)
                rewardObj.SetActive(false);
            if (turnSteelTimerObj)
                turnSteelTimerObj.SetActive(true);
        }
    }

    //�̺�Ʈ ���
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, OnStartTimer);
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, OnStopTimer);
    }

    //�̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, OnStartTimer);
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, OnStopTimer);
    }

    //Ÿ�̸� �۵� �Լ�
    private void OnStartTimer()
    {
        timerCoroutine = StartCoroutine(TurnSteelTimer(timerTime));
    }

    private void OnStopTimer()
    {
        if(timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }

    //Ÿ�̸� ���� �ڷ�ƾ �Լ�
    private IEnumerator TurnSteelTimer(float timerTime)
    {
        float nowTimerTime = timerTime;
        while (nowTimerTime > 0)
        {
            nowTimerTime -= Time.deltaTime;
            nowTimerTime = Mathf.Clamp(nowTimerTime, 0, timerTime); // �ּҰ� 0, �ִ밪 maxHealth�� ����
            //fillAmount����
            timerBar.fillAmount = nowTimerTime / timerTime;

            yield return null;
        }
    }
}
