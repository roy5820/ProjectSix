using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    //행동명으로 행동 실행
    public bool ActionExeToName(string actionName)
    {
        bool ron = false;//실행 여부

        ActionInfo getAction = actionList.Find(x => x.actionName == actionName);//액션리스트의 입력받은 액션명과 같은 액션을 찾기
        //null체크
        if (getAction != null)
        {

        }


        return ron;//실행여부 반환
    }
}
