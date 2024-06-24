using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipInfoIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CheepInfo chipInfo;
    public GameObject chipInspectorPre;//������ ���� â
    private GameObject popupPre = null;//������ �˾�â

    private void Start()
    {
        //Ĩ������ �䵥�� �� ����
        if(chipInfo != null) {
            GetComponent<Image>().sprite = chipInfo.CheepIcon;
        }
    }

    //���콺 �����Ͱ� �ö� �� ��� ������ �ν����� â�� ���� �̺�Ʈ
    public void OnPointerEnter(PointerEventData eventData)
    {
        chipInspectorPre.GetComponent<ChipInfoWIndow>().chipInfo = chipInfo;
        popupPre = Instantiate(chipInspectorPre, this.transform.parent.parent);
    }

    //���콺 �����Ͱ� ���� �� ��� ������ �ν����� â�� ���ִ� �̺�Ʈ
    public void OnPointerExit(PointerEventData eventData)
    {
        if (popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }
    }
}
