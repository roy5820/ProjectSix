using System.Collections;
using UnityEngine;
//ĳ���� ���� ��ȯ
public class TurnaboutState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //���� ��ȯ ��� ����
        characterController.Direction = characterController.Direction == CharacterDirection.Right ? CharacterDirection.Left : CharacterDirection.Right;

        yield return new WaitForSeconds(sateDelayTime);//������ȯ �� ������

        characterController.TurnEnd();//���� ���� �� �� ����

        yield return base.StateFuntion(datas);
    }
}

