using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
//캐릭터 이동시 데미지 부여
public class DamageWhenMovingState : MoveState
{
    public int damage = 10;//이동 시 입는 데미지

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //데미지 부여, 최소 체력 1
        characterController.NowHp -= damage;
        if(characterController.NowHp == 0)
            characterController.NowHp = 1;

        yield return base.StateFuntion(datas);
    }
}

