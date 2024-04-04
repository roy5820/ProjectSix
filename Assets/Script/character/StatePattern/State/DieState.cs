using System.Collections;
using UnityEngine;

public class DieState : StateBase
{
    protected override IEnumerator SateFuntion()
    {
        //캐릭터 죽음 기능 구현
        Debug.Log(gameObject.name + " is Die");
        yield return base.SateFuntion();
    }
}

