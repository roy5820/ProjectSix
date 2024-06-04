using System.Collections;
using UnityEngine;
//ĳ���� �ǰ�
public class HitState : StateBase
{
    private int hitDamage { get; set; }//�ǰ� �� ����� ������
    public bool cameraShake = false;//ȭ�� ��鸲 ����
    public float shakeTime = 0.3f;//��鸲 ���� �ð�
    public float shakePower = 20f;//��鸲 �Ŀ�

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //ü���� 0 �̻��� �� �ǰ� ó��
        if (characterController.NowHp > 0)
        {
            hitDamage = (int)datas[0];
            Debug.Log("Hit: " + hitDamage);
            //�ǰ� ó�� ��� ����
            characterController.NowHp -= hitDamage;//������ ���
            Debug.Log(gameObject.name + " is Hit, nowDamage: " + hitDamage + " nowHp: " + characterController.NowHp);

            //ȭ�� ��鸲 ����
            if (cameraShake)
                CameraController.Instance.OnShake(shakeTime, shakePower);

            yield return new WaitForSeconds(sateDelayTime);//�ִϸ��̼� ����� ���� ������

            //ü���� 0�̸� ���� ó��
            if (characterController.NowHp == 0)
            {
                characterController.isAvailabilityOfAction = false;
                characterController.TransitionState(StateEnum.Die);
                yield break;
            }
        }
        

        yield return base.StateFuntion(datas);
    }
}

