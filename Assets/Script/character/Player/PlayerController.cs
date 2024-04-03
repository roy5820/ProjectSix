using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{


    //플레이어 턴 종료 및 적 턴으로 전환 하는 이벤트를 발생 시키는 함수
    public void PlayerTurnEnd()
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
        if (GUI.Button(new Rect(20, 100, 200, 30), "MoveRight"))
        {
            MoveState(CharacterDirection.Right);
        }

        if (GUI.Button(new Rect(20, 140, 200, 30), "TurnaboutState"))
        {
            TurnaboutState();
        }

        if (GUI.Button(new Rect(20, 180, 200, 30), "HitState"))
        {
            HitState();
        }

        if (GUI.Button(new Rect(20, 220, 200, 30), "DieState"))
        {
            DieState();
        }
    }
}