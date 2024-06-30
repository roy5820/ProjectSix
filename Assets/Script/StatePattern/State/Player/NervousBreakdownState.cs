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
            while (true)
            {
                
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
                {
                    if (normalAni)
                        _animator.runtimeAnimatorController = normalAni;
                    playerController.isBreakdown = false;
                    characterController.TurnEnd();
                    break; 
                }
                yield return null;
            }
            
        }
        //���� ���� ó��
        else
        {
            if (nervousBreakdwonAni)
                _animator.runtimeAnimatorController = nervousBreakdwonAni;
            playerController.isBreakdown = true;
        }

        yield return base.StateFuntion(datas);
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
