using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�Ű�� ���� ������ ����
public class NervousBreakdownState : StateBase
{
    private Coroutine endCheckCoroutine = null;//
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//������ ����

        PlayerController playerController = (PlayerController)characterController;//�÷��̾� ��Ʈ�ѷ� ��������
        //���� ���ο� ���� ȿ�� ó��
        //���� ���� ó��
        if (playerController.isBreakdown)
        {
            characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            playerController.isBreakdown = false;
            StopCoroutine(endCheckCoroutine);
            characterController.TurnEnd();
        }
        //���� ���� ó��
        else
        {
            characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            playerController.isBreakdown = true;
            endCheckCoroutine = StartCoroutine(BreakdownEndCheck());
        }

        yield return base.StateFuntion(datas);
    }

    //�Ű� ������ ���͸� 0�� �ɽ� ������ ȿ�� ���� ��Ű�� �Լ�
    private IEnumerator BreakdownEndCheck()
    {
        PlayerController playerController = (PlayerController)characterController;//�÷��̾� ��Ʈ�ѷ� ��������
        while (playerController.NowBattery > 0)
        {
            yield return null;
        }
        StartCoroutine(StateFuntion());//�Ű� ���� ����
    }
}
