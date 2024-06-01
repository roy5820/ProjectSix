using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

public class PlayerTurnSteal : MonoBehaviour
{
    private BattleManager _battleManager;//��Ʋ�Ŵ���
    public float steelCastingTime = 10;//��ų �ð�
    private bool isPlayerTurn = false;//�÷��̾� �� ����
    private Coroutine stillTurnCoroutine = null;//�� ��ƿ ������ �ڷ�ƾ�� ��� ����

    private void Start()
    {
        _battleManager = BattleManager.Instance;//��Ʋ�Ŵ��� �� �ʱ�ȭ
    }

    //�̺�Ʈ ���
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurnStart);
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, PlayerTurnEnd);
    }

    //�̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurnStart);
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, PlayerTurnEnd);
    }

    //�÷��̾� �� ���� �� �̺�Ʈ ó�� �Լ�
    private void PlayerTurnStart()
    {
        stillTurnCoroutine = StartCoroutine(StillTUrn());//�÷��̾� �� ��ƿ ���� �ڷ�ƾ ȣ��
    }

    //�÷��̾� �� ���� �� �̺�Ʈ ó�� �Լ�
    private void PlayerTurnEnd()
    {
        //�ڷ�ƾ ���� ���� ��� ���� ����
        if (stillTurnCoroutine != null)
            StopCoroutine(stillTurnCoroutine);
    }

    //�� ��ƿ ���� �ڷ�ƾ �Լ�
    private IEnumerator StillTUrn()
    {
        yield return new WaitForSeconds(steelCastingTime);//�� ��ƿ ĳ���� ����
        TurnEventBus.Publish(TurnEventType.EnemyTurn);//�÷��̾� �� ����
    }
}
