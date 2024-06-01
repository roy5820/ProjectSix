using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndRunState : MoveState
{
    public string dashAniParamater = "";//�뽬 �ִϸ��̼�
    public float dashDelay = 0.1f;//��뽬 ���� �� ������
    public float powerCoefficient = 1.2f;//���ݷ� ���
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//�ִϸ��̼� ����� ���� ������
        //���� ��� ���� ���� ������ �� ����
        if (datas.Length > 0)
            powerCoefficient = (float)datas[0];
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//����� ������ ���ϱ�

        CharacterDirection characterDir = characterController.Direction;//ĳ���� ���Ⱑ������

        int onIndex = _battleManager.GetPlatformIndexForObj(characterController.gameObject);
        int attackIndex = onIndex + ((int)characterDir);//���� �� �÷��� index

        //���� ���� ����
        if (attackIndex >= 0 && attackIndex < _battleManager.PlatformList.Length)
        {
            //������ ��� �κ�
            _battleManager.GiveDamage(attackIndex, thisDamage);
        }

        //ĳ���� ���� ���� ���� 
        CharacterDirection runDirection = 
            (characterController.Direction == CharacterDirection.Left ? CharacterDirection.Right : CharacterDirection.Left);
        _animator.SetBool("IsFront", false);//�ִϸ��̼� �̵� ���� ����
        _animator.SetTrigger(dashAniParamater);//��뽬 �ִϸ��̼� ���
        yield return new WaitForSeconds(dashDelay);//�뽬 ������
        yield return base.StateFuntion(runDirection);//���� ����
    }
}
