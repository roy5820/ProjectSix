using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RushAttack : NormalAttackState
{
    public float dashSpeed = 60f;//대쉬 속도
    public int dashRange = 2;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        
        CharacterMovement movement = GetComponent<CharacterMovement>();//캐릭터 무브먼트 가져오기
        CharacterDirection dashDirection = characterController.direction;//캐릭터 돌진 방향 설정
        movement.moveCoroutine = StartCoroutine(movement.StraightLineMovement((int)dashDirection, dashSpeed, dashRange));//캐릭터 무브먼트를 사용하여 이동 구현

        //이동중 턴 종료 방지
        while (movement.moveCoroutine != null)
        {
            yield return null;
        }
        yield return base.StateFuntion(datas);
    }
}
