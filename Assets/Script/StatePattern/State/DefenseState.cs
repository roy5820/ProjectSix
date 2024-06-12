using System;
using System.Collections;
using UnityEngine;
//ĳ���� �ǰ�
public class DefenseState : StateBase
{
    private bool isDefense = false;//��� ����
    int defenseTurn = 1;//���� �ð�
    public GameObject effectPre;//��ȣ�� ǥ�� ����Ʈ
    public Transform effectPos;//����Ʈ ��ȯ ��ġ

    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        isDefense = true;
        characterController.isInvincibility = true;

        if(datas.Length > 0)
            defenseTurn = Convert.ToInt32(datas[0]);
        
        GameObject shiledPre = Instantiate(effectPre, effectPos.position, Quaternion.identity, characterController.transform);//��ȣ�� ����Ʈ ����

        yield return new WaitForSeconds(sateDelayTime);

        //���� ���·� ��ȯ �� �� ���� ó��
        characterController.isStatusProcessing = false;
        yield return new WaitForSeconds(sateDelayTime);//������
        characterController.TurnEnd();


        while (defenseTurn > 0)
        {
            yield return null;
        }
        Destroy(shiledPre);//����Ʈ ����
        characterController.isInvincibility = false;
        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isDefense = false;
        yield return base.StateFuntion(datas);
    }

    //�� ���ῡ ���� ���� ����
    private void TurnEnd()
    {
        if (isDefense)
        {
            defenseTurn--;
        }
            
    }
}

