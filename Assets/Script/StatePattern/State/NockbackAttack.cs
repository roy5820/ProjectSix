using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//ĳ���� �Ϲݰ���
public class NockbackAttack : StateBase
{
    public float powerCoefficient = 1.0f;
    public float nockBackPower = 100f;
    public bool cameraShake = false;//ȭ�� ��鸲 ����
    public float shakeTime = 0.3f;//��鸲 ���� �ð�
    public float shakePower = 20f;//��鸲 �Ŀ�
    public GameObject effectPre;//��ȣ�� ǥ�� ����Ʈ
    public Transform effectPos;//����Ʈ ��ȯ ��ġ
    public float effectLifeTime = 0.4f;//����Ʈ ���� �ֱ�
    private GameObject nowEffect = null;//������ ����Ʈ
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //���� ����
        yield return new WaitForSeconds(sateDelayTime);//�ִϸ��̼� ����� ���� ������
        //�߻� ����Ʈ ����
        if (effectPre != null)
        {
            nowEffect = Instantiate(effectPre, effectPos.position, Quaternion.identity);
            Invoke("DestroyEffect", effectLifeTime);
        }
            
        //ȭ�� ��鸲 ����
        if (cameraShake)
            CameraController.Instance.OnShake(shakeTime, shakePower);
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

            //�˹� ����
            GameObject targetObj = _battleManager.GetOnPlatformObj(attackIndex);

            if(targetObj != null)
            {
                CharacterMovement movement = targetObj.GetComponent<CharacterMovement>();//ĳ���� �����Ʈ ��������
                CharacterDirection moveDirection = characterController.Direction;//�Է� ���� ĳ���� �̵����� ����
                movement.moveCoroutine = StartCoroutine(movement.StraightLineMovement((int)moveDirection, nockBackPower, 1));//ĳ���� �����Ʈ�� ����Ͽ� �̵� ����

                //�̵��� �� ���� ����
                while (movement.moveCoroutine != null)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(sateDelayTime);
            }
        }
        characterController.TurnEnd();//���� ���� �� �� ����
        yield return base.StateFuntion(datas);
    }

    private void DestroyEffect()
    {
        Destroy(nowEffect);
    }
}
