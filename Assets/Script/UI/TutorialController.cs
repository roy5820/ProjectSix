using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Button LBtn;
    public Button RBtn;
    public Image chageImg = null;//화면에 보여줄 튜토리얼 이미지
    public List<Sprite> tutorialImgs = new List<Sprite>();
    private int nowPage = 0;

    private void Start()
    {
        SetPage();
    }

    //페이지 이동 버튼 클릭시 이벤트 처리
    public void OnNxtPageBtn(int value)
    {
        nowPage = (int)Mathf.Lerp(0, nowPage + value, tutorialImgs.Count-1);
        Debug.Log(nowPage + value);
        Debug.Log(nowPage);
        SetPage();
    }

    //페이지 재설정 함수
    private void SetPage()
    {
        chageImg.sprite = tutorialImgs[nowPage];

        if(nowPage == 0)
            LBtn.gameObject.SetActive(false);
        else 
            LBtn.gameObject.SetActive(true);

        if (nowPage >= tutorialImgs.Count-1)
            RBtn.gameObject.SetActive(false);
        else
            RBtn.gameObject.SetActive(true);
    }
}
