using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBtn : MonoBehaviour
{
    private GameManager _gameManager;//게임 메니저
    public int chipID = -1;//제거 및 추가할 칩 ID
    public int chipIndex = -1;//수정할 칩 인덱스 값
    private void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화
    }

    //칩 인벤토리에 추가하는 함수
    public void OnInsertChip()
    {

    }
    //칩 인벤토리에 제거하는 함수
    public void OnDelateChip()
    {
        _gameManager.SetCheepList(chipIndex, chipID);
    }
}
