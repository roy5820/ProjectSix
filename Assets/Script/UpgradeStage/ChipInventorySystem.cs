using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//칩장착창 UI 구현
public class ChipInventorySystem : MonoBehaviour
{
    private GameManager _gameManager = null;//게임 메니저

    //마우스 오버 중인 칩정보 표시관련 전역 변수
    public Image chipIconImage = null;//칩아이콘 표현할 이미지 객체
    public Text chipName = null;//칩 이름 표시할 텍스트 객체
    public Text chipEffect = null;//칩 효과 표시할 텍스트 객체

    //칩 버튼 생성을 위한 전역 변수들
    public float btnInterval = 125;//버튼 간격
    private List<CheepInfo> notHeldChipList = new List<CheepInfo>();//미보유 칩 리스트
    private List<GameObject> notHeldChipBtnList = new List<GameObject>();//미보유 칩 버튼 리스트
    public GameObject InsertChipBtnPre;//삽입 버튼
    public GameObject cheepMountedPanel = null;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화

        SetChipBtn();//버튼 초기화 코루틴 함수 호출
    }

    //cheep리스트를 바탕으로 칩 버튼 구성하는 코루틴 함수
    public void SetChipBtn()
    {
        //기존 Insert버튼 지우기
        while (notHeldChipBtnList.Count > 0)
        {
            Destroy(notHeldChipBtnList[0]);
            notHeldChipBtnList.RemoveAt(0);
            notHeldChipList.RemoveAt(0);
        }

        notHeldChipList.AddRange(_gameManager.cheepDataBase);//칩 데이터 가져오기

        List<int> cheepInventory = _gameManager.cheepInventory;//보유 칩 정보 가져오기

        //보유칩 정보를 바탕으로 칩 장착 버튼 생성
        //notHeldChipList에서 보유칩 제거
        foreach (int id in cheepInventory)
        {
            int delIndex = notHeldChipList.FindIndex(chip => chip.CheepID.Equals(id));
            if (delIndex >= 0)
                notHeldChipList.RemoveAt(delIndex);
        }

        //notHeldChipList을 바탕으로 버튼 생성
        // 버튼의 총 개수를 이용해 처음 버튼의 위치 계산
        float startX = -(notHeldChipList.Count - 1) * btnInterval / 2f;

        for (int i = 0; i < notHeldChipList.Count; i++)
        {
            //아이템 정보 갱신 후 프리펩 생성
            InsertChipBtnPre.GetComponent<ChipBtnBase>().chipInfo = notHeldChipList[i];
            GameObject chipBtn = Instantiate(InsertChipBtnPre, Vector3.zero, Quaternion.identity, cheepMountedPanel.transform);

            notHeldChipBtnList.Add(chipBtn);//리스트에 추가

            // 버튼의 위치 계산
            float posX = startX + i * btnInterval;

            // 버튼의 RectTransform 가져와서 위치 설정
            RectTransform rectTransform = chipBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, 0);
        }
    }

    //칩 정보창 업데이트 함수 chipInfo: 적용할 칩의 정보
    public void UpdateChipInfo(CheepInfo chipInfo)
    {
        if(chipInfo != null)
        {
            chipIconImage.sprite = chipInfo.CheepIcon;//칩정보 갱신
            chipName.text = chipInfo.CheepName;//칩 이름
            chipEffect.text = chipInfo.CheepEffectExplanationTxt;
        }
    }
}
