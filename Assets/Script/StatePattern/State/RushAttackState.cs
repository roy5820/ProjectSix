using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RushAttackState : NormalAttackState
{
    public float dashSpeed = 60f;//�뽬 �ӵ�
    public int dashRange = 4;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        CharacterMovement movement = characterController.GetComponent<CharacterMovement>();//ĳ���� �����Ʈ ��������
        CharacterDirection dashDirection = characterController.Direction;//ĳ���� ���� ���� ����
        movement.moveCoroutine = StartCoroutine(movement.StraightLineMovement((int)dashDirection, dashSpeed, dashRange));//ĳ���� �����Ʈ�� ����Ͽ� �̵� ����

        //�̵��� �� ���� ����
        while (movement.moveCoroutine != null)
        {
            yield return null;
        }

        yield return base.StateFuntion(datas);//���� ����
    }
}
