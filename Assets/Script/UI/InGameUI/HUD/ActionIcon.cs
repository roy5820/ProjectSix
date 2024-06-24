using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string actionName = null;
    public string actionEffect = null;
    public GameObject actionInspectorPopup = null;
    private GameObject popupPre = null;

    private void OnDisable()
    {
        if (popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }

    }

    //마우스 포인터가 올라 올 경우 아이템 인스펙터 창을 띄우는 이벤트
    public void OnPointerEnter(PointerEventData eventData)
    {
        actionInspectorPopup.GetComponent<EnemyActionInfoWindow>().actionName = actionName;
        actionInspectorPopup.GetComponent<EnemyActionInfoWindow>().actionEffect = actionEffect;
        popupPre = Instantiate(actionInspectorPopup, GameObject.Find("InGameUI").transform);
    }

    //마우스 포인터가 벗어 날 경우 앙이템 인스펙터 창을 없애는 이벤트
    public void OnPointerExit(PointerEventData eventData)
    {
        if (popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }
    }
}
