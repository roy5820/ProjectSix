using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//칩장착창 UI 구현
public class ChipInventorySystem : MonoBehaviour
{
    private GameManager _gameManager = null;//게임 메니저

    //마우스 오버 중인 칩정보 표시관련 전역 변수

    //칩 버튼 생성을 위한 전역 변수들
    private List<CheepInfo> notHeldChipList;//미보유 칩 리스트
    private List<GameObject> notHeldChipBtnList;//미보유 칩 버튼 리스트
    public GameObject InsertChipBtnPre;//
    private List<CheepInfo> heldChipList;//보유 칩 리스트
    public List<GameObject> heldChipBtnList;//보유 칩 버튼 리스트
    

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화

        SetChipBtn();//버튼 초기화
    }

    //cheep리스트를 바탕으로 칩 버튼 구성하는 함수
    private void SetChipBtn()
    {
        //기존 Insert버튼 지우기
        while(notHeldChipBtnList.Count > 0)
        {
            Destroy(notHeldChipBtnList[0]);
            notHeldChipBtnList.RemoveAt(0);
        }

        heldChipList = notHeldChipList = _gameManager.cheepDataBase;//칩 데이터 가져오기

        List<int> cheepInventory = _gameManager.cheepInventory;//보유 칩 정보 가져오기



    }
}
