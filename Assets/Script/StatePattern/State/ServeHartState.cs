using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//서브 하트 칩 기능 구현(죽었다 부활하는 기능)ㅂ
public class ServeHartState : DieState
{
    public int serveHartCnt = 1;//부활 횟수
    public float recoveryRate = 0.5f;//부활 시 체력회복 비율
    public float respawnDelay = 0.3f;//리스폰 시 딜레이

    public float deadDelay = 1.2f;//죽을 시 딜레이
    public string deadAniPara = "DieState";//죽을 시 재생할 애니 파라미터
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //부활 가능 여부 체크
        if (serveHartCnt > 0)
        {
            yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
            serveHartCnt--;//부활 횟수 감소
            characterController._characterStatus.nowHp = (int)(characterController._characterStatus.maxHp * recoveryRate);//체력 회복
            yield return new WaitForSeconds(respawnDelay);//리스폰 시 딜레이 구현
            characterController.isStatusProcessing = false;//캐릭터 상태 처리 종료
            yield break;
        }
        _animator.SetTrigger(deadAniPara);//죽을 시 애니 재생
        yield return new WaitForSeconds(deadDelay);//죽을 시 딜레이 구현

        //불가능 시 죽음 상태 호출
        yield return base.StateFuntion(datas);
    }
}