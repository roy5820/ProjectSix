using System;
using System.Collections;
using UnityEngine;
//캐릭터 피격
public class DefenseState : StateBase
{
    private bool isDefense = false;//방어 여부
    int defenseTurn = 1;//스턴 시간
    public GameObject effectPre;//보호막 표현 이펙트
    public Transform effectPos;//이펙트 소환 위치

    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 설정
    }

    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 제거
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        isDefense = true;
        characterController.isInvincibility = true;

        if(datas.Length > 0)
            defenseTurn = Convert.ToInt32(datas[0]);
        
        GameObject shiledPre = Instantiate(effectPre, effectPos.position, Quaternion.identity, characterController.transform);//보호막 이펙트 생성

        yield return new WaitForSeconds(sateDelayTime);

        //스턴 상태로 전환 후 턴 종료 처리
        characterController.isStatusProcessing = false;
        yield return new WaitForSeconds(sateDelayTime);//딜레이
        characterController.TurnEnd();


        while (defenseTurn > 0)
        {
            yield return null;
        }
        Destroy(shiledPre);//이펙트 제거
        characterController.isInvincibility = false;
        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isDefense = false;
        yield return base.StateFuntion(datas);
    }

    //턴 종료에 따른 스턴 관리
    private void TurnEnd()
    {
        if (isDefense)
        {
            defenseTurn--;
        }
            
    }
}

