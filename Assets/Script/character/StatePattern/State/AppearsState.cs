using System.Collections;
using UnityEngine;

public class AppearsState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //캐릭터 스폰 시 애니메이션 기능 구현
        Debug.Log(gameObject.name + " is Appears");
        yield return null;
    }
}
