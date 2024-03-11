using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    Coroutine actionCoroutine;//행동 시 작동할 코루틴이 담기는 변수

    public float beforTime = 0;//행동 선딜 시간
    public float exeTime = 0;//행동 실행 시간
    public float afterTime = 0;//행동 후딜 시간
    public int coolTimeTurn = 0;//행동 재사용에 필요한 턴

    //행동 실행 가능 여부 체크
    //자세한 내용은  자식에서 설정
    public virtual bool RadyOrNot()
    {
        return false;
    }

    //액션 준비여부에 따라 액션 실행
    public bool ActionStart()
    {
        bool isAction = false;//실행 여부
        if (RadyOrNot())
        {
            actionCoroutine = StartCoroutine(BeforAction());//액션 시작
            isAction = true;//액션 실행 여부 true
        }

        return isAction;//액션 실행여부 반환
    }

    //행동 선딜레이 구현 부분
    protected IEnumerator BeforAction()
    {
        //딜레이 구현
        if (beforTime != 0)
            yield return new WaitForSeconds(beforTime);

        actionCoroutine = StartCoroutine(ExeAction());//스킬구현 코루틴 호출
        yield return null;
    }

    //행동 구현 부분
    protected IEnumerator ExeAction()
    {
        //딜레이 구현
        if (exeTime != 0)
            yield return new WaitForSeconds(exeTime);

        actionCoroutine = StartCoroutine(AfterAction());//스킬 후딜레이 구현
        yield return null;
    }

    //행동 후딜레이 부분
    protected IEnumerator AfterAction()
    {
        //딜레이 구현
        if (afterTime != 0)
            yield return new WaitForSeconds(afterTime);

        actionCoroutine = null;
        yield return null;
    }

    //행동 쿨타임 구현 부분
    protected virtual IEnumerator ActionCool()
    {
        yield return null;
    }
}
