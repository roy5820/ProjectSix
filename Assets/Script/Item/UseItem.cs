using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerController _playerController;
    public ItemInfo itemInfo;
    public Image itemImg;//������ �̹���
    public Text itemText;//�ڽ�Ʈ �ؽ�Ʈ
    public StateEnum useState;//������ ���� ����� ����
    public float offense = 1f;//���
    public int useCost = 5;//��� �� ���� �ڽ�Ʈ
    public GameObject itemInspectorPre;//������ ���� â
    private GameObject popupPre = null;//������ �˾�â

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//�÷��̾� ��ü ��������
        //itemInfo�� ������ ���� �� �ʱ�ȭ
        if(itemInfo != null)
        {
            itemImg.sprite = itemInfo.itemImg;//������ �̹���=
            offense = itemInfo.offense;//������ ���
            //���� �ڽ�Ʈ
            useCost = itemInfo.useCost;
            itemText.text = itemInfo.useCost.ToString();
            useState = itemInfo.state;//���� ����
        }
    }

    //������ ��� ó��
    public void ClickItem()
    {
        //������ ��� ���� ���� üũ
        if (_playerController)
        {
            if (!_playerController.isStatusProcessing && _playerController.isTurnReady && (_playerController.nowBattery >= useCost || _playerController.isBreakdown))
            {
                //���� ���°� �ƴҰ�� �ڽ�Ʈ ����
                if (!_playerController.isBreakdown)
                    _playerController.nowBattery -= useCost;//�ڽ�Ʈ ����
                _playerController.TransitionState(useState, offense);//������ ���
            }
        }
    }

    //���콺 �����Ͱ� �ö� �� ��� ������ �ν����� â�� ���� �̺�Ʈ
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInspectorPre.GetComponent<ItemInspectorPopup>().itemInfo = itemInfo;
        popupPre = Instantiate(itemInspectorPre, this.transform.parent.parent.parent);
    }

    //���콺 �����Ͱ� ���� �� ��� ������ �ν����� â�� ���ִ� �̺�Ʈ
    public void OnPointerExit(PointerEventData eventData)
    {
        if(popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }
    }
}