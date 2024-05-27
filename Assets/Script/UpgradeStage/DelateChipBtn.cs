using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//보유 칩 버튼 구현
public class DelateChipBtn : MonoBehaviour
{
    private GameManager _gameManager;//게임 메니저
    public int chipIndex = -1;//수정할 칩 인덱스 값
    private void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화
    }

    //칩 인벤토리에 제거하는 함수
    public void OnDelateChip()
    {
        if (chipIndex > 0 && chipIndex < _gameManager.cheepInventory.Count)
        {

        }
    }
}
