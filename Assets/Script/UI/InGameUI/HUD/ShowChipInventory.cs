using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChipInventory : MonoBehaviour
{
    private GameManager _gameManager = null;//게임 메니저
    public GameObject chipIconPre = null;//칩 아이콘 프리펩
    public float chipInterval = 100;//칩 간격

    void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화

        SetChipInventory();//버튼 초기화 코루틴 함수 호출
    }

    private void SetChipInventory()
    {
        List<CheepInfo> heldChipList = new List<CheepInfo>();//보유 칩 리스트

        List<CheepInfo> chipDatas = _gameManager.cheepDataBase;//칩 데이터
        List<int> cheepInventory = _gameManager.cheepInventory;//보유 칩 정보 가져오기

        //보유칩 정보를 바탕으로 칩 아이콘 생성
        //
        foreach (int id in cheepInventory)
        {
            int addIndex = chipDatas.FindIndex(chip => chip.CheepID.Equals(id));
            if (addIndex >= 0)
                heldChipList.Add(chipDatas[addIndex]);
        }

        //칩 아이콘 생성 부분
        float startX = -(heldChipList.Count - 1) * chipInterval / 2f;

        for (int i = 0; i < heldChipList.Count; i++)
        {
            
            GameObject chipBtn = Instantiate(chipIconPre, transform);//프리펩 생성
            chipBtn.GetComponent<ChipInfoIcon>().chipInfo = heldChipList[i];
            // 칩 아이콘 위치 계산
            float posX = startX + i * chipInterval;

            // 칩의 RectTransform 가져와서 위치 설정
            RectTransform rectTransform = chipBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, 0);
        }
    }
}