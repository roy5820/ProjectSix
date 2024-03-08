using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    Coroutine actionCoroutine;//행동 시 작동할 코루틴이 담기는 변수



    //행동 실행 가능 여부 체크
    public bool RadyOrNot()
    {
        bool ron = false;//반환할 준비여부



        return ron;
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

    //스킬 선딜레이 구현 부분
    IEnumerator BeforAction()
    {
        actionCoroutine = StartCoroutine(ExeAction());
        yield return null;
    }

    //스킬 구현 부분
    IEnumerator ExeAction()
    {
        actionCoroutine = StartCoroutine(AfterAction());
        yield return null;
    }

    //스킬 후딜레이 부분
    IEnumerator AfterAction()
    {
        actionCoroutine = null;
        yield return null;
    }
}
