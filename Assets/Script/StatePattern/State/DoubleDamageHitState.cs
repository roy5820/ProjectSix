using System.Collections;
using UnityEngine;
//ĳ���� �ǰ�
public class DoubleDamageHitState : HitState
{
    public float DamageAmplification = 2;//������ ������
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        int hitDamage = (int)((float)((int)datas[0]) * DamageAmplification);
        Debug.Log("double :"+hitDamage);

        yield return base.StateFuntion(hitDamage);
    }
}

