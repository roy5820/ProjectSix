using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ĳ���� �Ϲݰ���
public class BulletFireState : StateBase
{
    public float powerCoefficient = 1.0f;
    public GameObject bulletPre;//�߻��� �Ѿ� ������Ʈ
    public Transform firePoint;//�߻� ��ġ
    public GameObject fireEffectPre = null;//�߻�� ������ ����Ʈ
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        GameObject bullet = Instantiate(bulletPre, firePoint.position, Quaternion.identity);//�Ѿ� ������Ʈ ����
        //�߻� ����Ʈ ����
        GameObject fireEffect = null;
        if(fireEffectPre  != null)
            fireEffect = Instantiate(fireEffectPre, firePoint.position, Quaternion.identity);
        yield return new WaitForSeconds(sateDelayTime);//�ִϸ��̼� ����� ���� ������
        if(fireEffect != null)
            Destroy(fireEffect);
        //���� ��� ���� ���� ������ �� ����
        if (datas.Length > 0)
            powerCoefficient = (float)datas[0];
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//����� ������ ���ϱ�

        CharacterDirection characterDir = characterController.Direction;//�ٶ󺸴� ���Ⱑ������

        BulletBase bulletBase = bullet.GetComponent<BulletBase>();//�Ѿ� ������Ʈ��������
        bulletBase.fireDir = characterDir;//�߻� ���� ����
        bulletBase.damage = thisDamage;//������ ����
        bulletBase.OnFire();//�Ѿ� �߻�

        //�Ѿ��� �߻�Ǿ� �ı� �ɶ����� ���
        while (bullet != null)
        {
            yield return null;
        }

        characterController.TurnEnd();//���� ���� �� �� ����
        yield return base.StateFuntion(datas);
    }
}
