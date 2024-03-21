using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInfoManagement : MonoBehaviour
{
    public int indexNum = 0;//해당 플렛폼이 플렛폼 리스트의 몇번째 위치에 있는지 나타내는 인덱스 번호
    public LayerMask characterLayer;//플렛폼위에 올라올 수 있는 캐릭터의 레이어 값
    private GameObject onPlatformCharacter = null;//해당 플렛폼의 있는 캐릭터 obj
    public Transform standingObj = null;//플렛폼위에 설 위치

    //onPlatformCharacter 프로퍼티
    public GameObject OnPlatformCharacter
    {
        get
        {
            return onPlatformCharacter;
        }
    }

    //standingObj의 위치값 프로퍼티
    public Vector3 StandingPos
    {
        get
        {
            return standingObj.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //캐릭터가 플렛폼에 들어올때 처리
        if (((1 << collision.gameObject.layer) & characterLayer) != 0 )
        {
            onPlatformCharacter = collision.gameObject;//onPlatformCharacter에 들어온 캐릭터 obj값 저장
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        //캐릭터가 플렛폼에 나갈때 처리
        if (((1 << collision.gameObject.layer) & characterLayer) != 0)
        {
            onPlatformCharacter = null;//onPlatformCharacter 값 null로 초기화
        }
    }

    public static implicit operator PlatformInfoManagement(GameObject v)
    {
        throw new NotImplementedException();
    }
}