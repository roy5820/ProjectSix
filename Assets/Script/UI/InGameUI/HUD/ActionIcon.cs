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

    //���콺 �����Ͱ� �ö� �� ��� ������ �ν����� â�� ���� �̺�Ʈ
    public void OnPointerEnter(PointerEventData eventData)
    {
        actionInspectorPopup.GetComponent<EnemyActionInfoWindow>().actionName = actionName;
        actionInspectorPopup.GetComponent<EnemyActionInfoWindow>().actionEffect = actionEffect;
        popupPre = Instantiate(actionInspectorPopup, GameObject.Find("InGameUI").transform);
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
