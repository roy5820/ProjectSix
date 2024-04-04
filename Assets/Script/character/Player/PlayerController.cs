using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{


    //플레이어 턴 종료 처리
    public override void TurnEnd()
    {
        TurnEventBus.Publish(TurnEventType.EnemyTurn);
    }

    //
    private void OnGUI()
    {
        //등장
        if(GUI.Button(new Rect(20, 40, 200, 30), "AppearsState"))
        {
            AppearsState();
        }

        //이동 왼쪽
        if (GUI.Button(new Rect(20, 80, 200, 30), "MoveLeft"))
        {
            MoveState(CharacterDirection.Left);
        }

        //이동 오른쪽
        if (GUI.Button(new Rect(20, 120, 200, 30), "MoveRight"))
        {
            MoveState(CharacterDirection.Right);
        }

        //방량 전환
        if (GUI.Button(new Rect(20, 160, 200, 30), "TurnaboutState"))
        {
            TurnaboutState();
        }

        //피격
        if (GUI.Button(new Rect(20, 200, 200, 30), "HitState"))
        {
            HitState(50);
        }

        //죽음
        if (GUI.Button(new Rect(20, 240, 200, 30), "DieState"))
        {
            DieState();
        }

        //적 턴 강제 종료
        if(GUI.Button(new Rect(20, 280, 200, 30), "Enemy TUrnEnd"))
        {
            TurnEventBus.Publish(TurnEventType.TurnEnd);
        }
    }
}