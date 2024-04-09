using System.Collections;
using UnityEngine;

public class HitState : StateBase
{
    public int hitDamage { get; set; }//피격 시 계산할 데미지

    protected override IEnumerator StateFuntion()
    {
        
        //피격 처리 기능 구현
        characterController.NowHp = -hitDamage;//데미지 계산
        Debug.Log(gameObject.name + " is Hit, nowDamage: "+ hitDamage + " nowHp: " + characterController.NowHp);
        if (characterController.NowHp == 0)
            gameObject.GetComponent<CharacterController>().DieState();
        yield return null;
    }
}

