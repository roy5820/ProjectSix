using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Button LBtn;
    public Button RBtn;
    public Image chageImg = null;//ȭ�鿡 ������ Ʃ�丮�� �̹���
    public List<Sprite> tutorialImgs = new List<Sprite>();
    private int nowPage = 0;

    private void Start()
    {
        SetPage();
    }

    //������ �̵� ��ư Ŭ���� �̺�Ʈ ó��
    public void OnNxtPageBtn(int value)
    {
        nowPage = (int)Mathf.Lerp(0, nowPage + value, tutorialImgs.Count-1);
        Debug.Log(nowPage + value);
        Debug.Log(nowPage);
        SetPage();
    }

    //������ �缳�� �Լ�
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
