using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBatteryGauge : MonoBehaviour
{
    private PlayerController _playerController;//플레이어 컨트롤러

    public List<GameObject> batteryImageList;//버튼 리스트
    public float batteryInterval = 40;//배터리 간격
    public GameObject batteryPre;//배터리 프리펩
    public Sprite batteryOnImg;//배터리 켜졌을 때 이미지
    public Sprite batteryOffImg;//배터리 꺼졌을 때 이미지

    private Coroutine runningCoroutine = null;//현재 진행중인 코루틴

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//캐릭터 매니저 초기화

        StartCoroutine(SetMaxBattery());
    }

    private void Update()
    {
        //배터리 값 갱신
        if (_playerController)
        {
            

            //현재 배터리량 값 갱신
            for (int i = 0; i < batteryImageList.Count; i++)
            {
                Sprite batteryImg = i < _playerController.nowBattery ? batteryOnImg : batteryOffImg;//현재 배터리 수치에 따른 이미지 선택
                batteryImageList[i].GetComponent<Image>().sprite = batteryImg;  
            }
        }
    }

    public IEnumerator SetMaxBattery()
    {
        //최대 배터리 용량 만큼 
        // 버튼의 총 개수를 이용해 처음 버튼의 위치 계산
        float startX = -(_playerController._characterStatus.maxBattery - 1) * batteryInterval / 2f;
        //플레이어 아이템 DB를 바탕으로 아이템 사용 버튼 생성 및 배치
        for (int i = 0; i < _playerController._characterStatus.maxBattery; i++)
        {
            GameObject battery = Instantiate(batteryPre, transform);

            // 버튼의 위치 계산
            float posX = startX + i * batteryInterval;

            // battery의 RectTransform 가져와서 위치 설정
            RectTransform rectTransform = battery.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);

            //battery Image컴포넌트 리스트에 넣기
            batteryImageList.Add(battery);
        }
        runningCoroutine = null;
        yield return null;
    }
}
