using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInfoManagement : MonoBehaviour
{
    public LayerMask characterLayer;//플렛폼위에 올라올 수 있는 캐릭터의 레이어 값
    private GameObject onPlatformCharacter = null;//해당 플렛폼의 있는 캐릭터 obj

    //onPlatformCharacter값을 관리하는 프로퍼티
    public GameObject OnPlatformCharacter
    {
        get
        {
            return onPlatformCharacter;
        }
        set
        {
            onPlatformCharacter = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //캐릭터가 플렛폼에 들어올때 처리
        if (((1 << collision.gameObject.layer) & characterLayer) != 0 ){
            onPlatformCharacter.transform.SetParent(transform);//onPlatformCharacter를 해당 플렛폼의 자식으로 귀속
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
}