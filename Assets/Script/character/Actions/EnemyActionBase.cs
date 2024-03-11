using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionBase : ActionBase
{
    //행동 쿨타임 구현 부분
    protected override IEnumerator ActionCool()
    {

        yield return null;
    }
}
