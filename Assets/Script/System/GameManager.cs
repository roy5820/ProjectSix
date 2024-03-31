using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] PlatformList;//플랫폼 리스트

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        
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
        Vector2 returnPos = Vector2.zero;//반환할 위치 값

        //플렛폼 리스트의 유효한 인덱스 값인지 체크
        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnPos = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().StandingPos;
        }

        return returnPos;
    }
}