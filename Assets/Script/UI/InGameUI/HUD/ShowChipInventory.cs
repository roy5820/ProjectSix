using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChipInventory : MonoBehaviour
{
    private GameManager _gameManager = null;//���� �޴���
    public GameObject chipIconPre = null;//Ĩ ������ ������
    public float chipInterval = 100;//Ĩ ����

    void Start()
    {
        _gameManager = GameManager.Instance;//���Ӹ޴��� �ʱ�ȭ

        SetChipInventory();//��ư �ʱ�ȭ �ڷ�ƾ �Լ� ȣ��
    }

    private void SetChipInventory()
    {
        List<CheepInfo> heldChipList = new List<CheepInfo>();//���� Ĩ ����Ʈ

        List<CheepInfo> chipDatas = _gameManager.cheepDataBase;//Ĩ ������
        List<int> cheepInventory = _gameManager.cheepInventory;//���� Ĩ ���� ��������

        //����Ĩ ������ �������� Ĩ ������ ����
        //
        foreach (int id in cheepInventory)
        {
            int addIndex = chipDatas.FindIndex(chip => chip.CheepID.Equals(id));
            if (addIndex >= 0)
                heldChipList.Add(chipDatas[addIndex]);
        }

        //Ĩ ������ ���� �κ�
        float startX = -(heldChipList.Count - 1) * chipInterval / 2f;

        for (int i = 0; i < heldChipList.Count; i++)
        {
            
            GameObject chipBtn = Instantiate(chipIconPre, transform);//������ ����
            chipBtn.GetComponent<ChipInfoIcon>().chipInfo = heldChipList[i];
            // Ĩ ������ ��ġ ���
            float posX = startX + i * chipInterval;

            // Ĩ�� RectTransform �����ͼ� ��ġ ����
            RectTransform rectTransform = chipBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, 0);
        }
    }
}