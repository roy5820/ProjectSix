using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�Ű�� ���� ������ ����
public class NervousBreakdownState : StateBase
{
    private Coroutine endCheckCoroutine = null;//
    private PlayerController playerController = null;
    public RuntimeAnimatorController normalAni = null;
    public RuntimeAnimatorController nervousBreakdwonAni = null;//�Ű�� ���ֽ� �ִϸ��̙l��
    //�̺�Ʈ ���
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, StopBreakdown);
    }

    //�̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, StopBreakdown);
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        playerController = (PlayerController)characterController;//�÷��̾� ��Ʈ�ѷ� ��������

        //���� ���ο� ���� ȿ�� ó��
        BackgroundEffectController.Instance.OnSwitchBackgroundAfterimage();//��� �ܻ�ȿ�� ����
        //���� ���� ó��
        if (playerController.isBreakdown)
        {
            if (normalAni)
                _animator.runtimeAnimatorController = normalAni;
            playerController.isBreakdown = false;
            StopCoroutine(endCheckCoroutine);
            characterController.TurnEnd();
        }
        //���� ���� ó��
        else
        {
            if (nervousBreakdwonAni)
                _animator.runtimeAnimatorController = nervousBreakdwonAni;
            playerController.isBreakdown = true;
            endCheckCoroutine = StartCoroutine(BreakdownEndCheck());
        }

        yield return base.StateFuntion(datas);
    }

    //�Ű� ������ ���͸� 0�� �ɽ� ������ ȿ�� ���� ��Ű�� �Լ�
    private IEnumerator BreakdownEndCheck()
    {
        while (playerController.NowBattery > 0)
        {
            yield return null;
        }
        StartCoroutine(StateFuntion());//�Ű� ���� ����
    }

    //�Ű� ���� ���� ���� �Լ�
    private void StopBreakdown()
    {
        if (playerController != null)
        {
            if (playerController.isBreakdown)
                StartCoroutine(StateFuntion());//�Ű� ���� ����
        }
    }
}
