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
        if (characterController.NowHp == 0)
            gameObject.GetComponent<CharacterController>().TransitionState(StateEnum.Die);

        yield return null;
    }
}

