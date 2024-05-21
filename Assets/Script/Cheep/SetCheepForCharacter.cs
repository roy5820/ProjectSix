using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//칩인벤토리에 있는 칩정보를 바탕으로 칩기능을 구현하는 클래스
public class SetCheepForCharacter : MonoBehaviour
{
    GameManager _gameManager = null;//게임 메니저
    CharacterController _chracterController = null;//캐릭터 컨트롤러
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임 메니저 값 초기화
        _chracterController = GetComponent<CharacterController>();//캐릭터 컨트롤러 값 초기화

        //null체크
        if(_gameManager != null && _chracterController != null)
        {
            //칩 인벤토리의 칩 정보를 바탕으로 상태 타입별 상태 변경
            

        }
    }

}
