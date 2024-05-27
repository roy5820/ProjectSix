using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//칩 삽입 버튼 구현
public class InsertChipBtn : MonoBehaviour
{
    private GameManager _gameManager;//게임 메니저
    public CheepInfo chipInfo = null;// 현재칩 정보
    private void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화
    }

    //칩 인벤토리에 추가하는 함수
    public void OnInsertChip()
    {

    }
}
