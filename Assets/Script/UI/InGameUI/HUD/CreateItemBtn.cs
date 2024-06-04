using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateItemBtn : MonoBehaviour
{
    GameManager _gameManager;//���� �޴���
    private List<ItemInfo> itemList = new List<ItemInfo>();//�÷��̾ �������� ������ DB
    public GameObject itemCreationPanel = null;
    public float btnInterval = 40;//��ư ����
    public GameObject useItemBtnPre;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//���� �޴��� ��������
        itemList = _gameManager.OutputOfHaveItems();//�÷��̾ �������� ������ DB ��������

        // ����� �������� �����ϱ� ���� ȭ�� �ʺ��� ���� �� ���
        float centerX = 0;
        // ��ư�� �� ������ �̿��� ó�� ��ư�� ��ġ ���
        float startX = centerX - (itemList.Count - 1) * btnInterval / 2f;
        //�÷��̾� ������ DB�� �������� ������ ��� ��ư ���� �� ��ġ
        for (int i = 0; i < itemList.Count; i++)
        {
            //������ ���� ���� �� ������ ����
            useItemBtnPre.GetComponent<UseItem>().itemInfo = itemList[i];
            GameObject itemBtn = Instantiate(useItemBtnPre, itemCreationPanel.transform);


            // ��ư�� ��ġ ���
            float posX = startX + i * btnInterval;

            // ��ư�� RectTransform �����ͼ� ��ġ ����
            RectTransform rectTransform = itemBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);
        }
    }
}
