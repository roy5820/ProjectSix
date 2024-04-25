using System.Collections;
using UnityEngine;
//캐릭터 피격
public class HitState : StateBase
{
    private int hitDamage { get; set; }//피격 시 계산할 데미지

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        hitDamage = (int)datas[0];
        //피격 처리 기능 구현
        characterController.NowHp -= hitDamage;//데미지 계산
        Debug.Log(gameObject.name + " is Hit, nowDamage: "+ hitDamage + " nowHp: " + characterController.NowHp);

        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이

        //체력이 0이면 죽음 처리
        if (characterController.NowHp == 0)
        {
            characterController.isAvailabilityOfAction = false;
            characterController.TransitionState(StateEnum.Die);
            yield break;
        }

        yield return base.StateFuntion(datas);
    }
}

