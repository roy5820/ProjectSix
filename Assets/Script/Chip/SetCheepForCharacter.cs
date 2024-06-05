using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//칩인벤토리에 있는 칩정보를 바탕으로 칩기능을 구현하는 클래스
public class SetCheepForCharacter : MonoBehaviour
{
    GameManager _gameManager = null;//게임 메니저
    CharacterController _chracterController = null;//캐릭터 컨트롤러
    
    public List<CheepPair> cheepList = new List<CheepPair>();//칩 타입과 기능 구현 컴포넌트를 연결하는 리스트
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임 메니저 값 초기화
        _chracterController = GetComponent<CharacterController>();//캐릭터 컨트롤러 값 초기화

        //null체크
        if(_gameManager != null && _chracterController != null)
        {
            //칩 인벤토리의 칩 정보를 바탕으로 상태 타입별 상태 변경
            List<CheepInfo> cheepList = _gameManager.cheepDataBase;//칩정보 가져오기
            List<int> haveCheepIDS = _gameManager.cheepInventory;//보유중인 칩 ID가져오기
            //칩인벤토리에 있는 칩들의 ID를 바탕으로 칩 기능구현 하는 CHeepBase객체를 찾아 실행
            for(int i = 0; i < haveCheepIDS.Count; i++)
            {
                
                CheepInfo cheepInfo = cheepList.Find(cheep => cheep.CheepID.Equals(haveCheepIDS[i]));
                if(cheepInfo != null )
                {
                    CheepType cheepType = cheepList.Find(cheep => cheep.CheepID.Equals(haveCheepIDS[i])).cheepType;
                    TransitionCheep(cheepType);
                }
            }

        }
    }

    //칩 타입별로 칩 기능 구현 함수 호출
    public void TransitionCheep(CheepType cheepType)
    {
        CheepBase cheepComponent = cheepList.Find(cheep => cheep.cheepType.Equals(cheepType)).cheepComponent;//칩 타입에 따른 기능 구현 컴포넌트 가져오기
        //null체크 후 칩 실행
        if (cheepComponent != null)
            cheepComponent.ActivateChipEffect();
    }

}
