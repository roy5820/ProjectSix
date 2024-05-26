using System.Collections;
using UnityEngine;
//캐릭터 피격
public class DoubleDamageHitState : HitState
{
    public int DamageAmplification = 2;//데미지 증폭량
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        int hitDamage = (int)datas[0] * 2;
        Debug.Log("double :"+hitDamage);

        yield return base.StateFuntion(hitDamage);
    }
}

