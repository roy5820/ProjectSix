using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] PlatformList;//플랫폼 리스트

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        //이벤트 발생 시 호출할 함수 설정
        GameFlowEventBus.Subscribe(GameFlowType.Start, GameStart);
        GameFlowEventBus.Subscribe(GameFlowType.Rest, GameRest);
        GameFlowEventBus.Subscribe(GameFlowType.Lose, GameLose);
        GameFlowEventBus.Subscribe(GameFlowType.Win, GameWin);

    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        //이벤트 제거
        GameFlowEventBus.Unsubscribe(GameFlowType.Start, GameStart);
        GameFlowEventBus.Unsubscribe(GameFlowType.Rest, GameRest);
        GameFlowEventBus.Unsubscribe(GameFlowType.Lose, GameLose);
        GameFlowEventBus.Unsubscribe(GameFlowType.Win, GameWin);
    }

    private void Start()
    {
        GameFlowEventBus.Publish(GameFlowType.Start);//게임 시작 이벤트 발생
    }

    //특정 위치의 플렛폼안에 캐릭터obj의 정보 반환 함수
    public GameObject GetOnPlatformObj(int indexNum)
    {
        GameObject returnObj = null;//반환할 오브젝트 값

        //플렛폼 리스트의 유효한 인덱스 값인지 체크
        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnObj = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().OnPlatformCharacter;
        }
        return returnObj;
    }

    //특정 위치의 플렛폼안에  정보 반환 함수
    public Vector3 GetStandingPos(int indexNum)
    {
        Vector3 returnPos = Vector3.zero;//반환할 위치 값
        
        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnPos = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().StandingPos;
        }

        return returnPos;
    }

    //게임 시작시 이벤트 처리
    public void GameStart()
    {
        GameFlowEventBus.Publish(GameFlowType.Battle);
    }

    //업그레이드 단계 이벤트 처리
    public void GameRest()
    {
        
    }

    //게임 패배시 이벤트 처리
    public void GameLose()
    {
        Debug.Log("PlayerLose");
    }

    //게임 승리시 이벤트 처리
    public void GameWin()
    {
        Debug.Log("PlayerWin");
    }
}