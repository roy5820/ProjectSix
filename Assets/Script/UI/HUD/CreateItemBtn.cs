using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateItemBtn : MonoBehaviour
{
    GameManager _gameManager;//게임 메니저
    private List<ItemInfo> itemList = new List<ItemInfo>();//플레이어가 보유중인 아이템 DB
    public float btnInterval = 40;//버튼 간격
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임 메니저 가져오기
        itemList = _gameManager.playerItemDB;//플레이어가 보유중인 아이템 DB 가져오기

        // 가운데를 기준으로 정렬하기 위해 화면 너비의 절반 값 계산
        float centerX = 0;

        // 버튼의 총 개수를 이용해 처음 버튼의 위치 계산
        float startX = centerX - (itemList.Count - 1) * btnInterval / 2f;
        //플레이어 아이템 DB를 바탕으로 아이템 사용 버튼 생성 및 배치
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject itemBtn = Instantiate(itemList[i].itemObj, transform);

            // 버튼의 위치 계산
            float posX = startX + i * btnInterval;

            // 버튼의 RectTransform 가져와서 위치 설정
            RectTransform rectTransform = itemBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);
        }
    }
}
