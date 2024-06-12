using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChipBtnBase : MonoBehaviour, IPointerEnterHandler
{
    protected GameManager _gameManager;//���� �޴���
    public CheepInfo chipInfo = null;// ����Ĩ ����
    private Image chipIcon;//Ĩ�������� ������ �̹��� ��ü
    public Sprite nullIcon;//��Ĩ ������
    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;//���Ӹ޴��� �ʱ�ȭ
        chipIcon = transform.GetChild(0).GetComponent<Image>();//Ĩ������ ǥ���� �̹��� ��ü�� �ʱ�ȭ
    }

    //��ư ������ �̹��� ����
    protected virtual void Update()
    {
        //CheepInfo�� icon�̹��� ����
        if (chipInfo != null)
        {
            chipIcon.gameObject.SetActive(true);
            chipIcon.sprite = chipInfo.CheepIcon;
        }
        //nullIcon �̹��� ����
        else
        {
            chipIcon.gameObject.SetActive(false);
        }
    }

    //Ĩ ��ư Ŭ�� �� �̺�Ʈ ���� �Լ�
    public virtual void OnChipEvent()
    {
        
        //Ĩ �κ��丮 ������ ���� �� ȭ�� Ĩ ��ư �籸��
        ChipInventorySystem chipInventorySystem = GetComponentInParent<ChipInventorySystem>();
        
        chipInventorySystem.SetChipBtn();//Ĩ ��ư�� ���� �޴����� cheepInventory���� �������� �籸���ϴ� �Լ� ȣ��
    }

    //���콺 �����Ͱ� �ö� �� ��� Ĩ ����â ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(chipInfo != null)
        {
            ChipInventorySystem chipInventorySystem = GetComponentInParent<ChipInventorySystem>();//Ĩ�κ��丮 ���� �ý��� ��������
            chipInventorySystem.UpdateChipInfo(chipInfo);//Ĩ ����â ������Ʈ �Լ� ȣ��
        }    
    }
}
