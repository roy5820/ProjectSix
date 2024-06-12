using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ĩ����â UI ����
public class ChipInventorySystem : MonoBehaviour
{
    private GameManager _gameManager = null;//���� �޴���

    //���콺 ���� ���� Ĩ���� ǥ�ð��� ���� ����
    public Image chipIconImage = null;//Ĩ������ ǥ���� �̹��� ��ü
    public Text chipName = null;//Ĩ �̸� ǥ���� �ؽ�Ʈ ��ü
    public Text chipEffect = null;//Ĩ ȿ�� ǥ���� �ؽ�Ʈ ��ü

    //Ĩ ��ư ������ ���� ���� ������
    public float btnInterval = 125;//��ư ����
    private List<CheepInfo> notHeldChipList = new List<CheepInfo>();//�̺��� Ĩ ����Ʈ
    private List<GameObject> notHeldChipBtnList = new List<GameObject>();//�̺��� Ĩ ��ư ����Ʈ
    public GameObject InsertChipBtnPre;//���� ��ư
    public GameObject cheepMountedPanel = null;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//���Ӹ޴��� �ʱ�ȭ

        SetChipBtn();//��ư �ʱ�ȭ �ڷ�ƾ �Լ� ȣ��
    }

    //cheep����Ʈ�� �������� Ĩ ��ư �����ϴ� �ڷ�ƾ �Լ�
    public void SetChipBtn()
    {
        //���� Insert��ư �����
        while (notHeldChipBtnList.Count > 0)
        {
            Destroy(notHeldChipBtnList[0]);
            notHeldChipBtnList.RemoveAt(0);
            notHeldChipList.RemoveAt(0);
        }

        notHeldChipList.AddRange(_gameManager.cheepDataBase);//Ĩ ������ ��������

        List<int> cheepInventory = _gameManager.cheepInventory;//���� Ĩ ���� ��������

        //����Ĩ ������ �������� Ĩ ���� ��ư ����
        //notHeldChipList���� ����Ĩ ����
        foreach (int id in cheepInventory)
        {
            int delIndex = notHeldChipList.FindIndex(chip => chip.CheepID.Equals(id));
            if (delIndex >= 0)
                notHeldChipList.RemoveAt(delIndex);
        }

        //notHeldChipList�� �������� ��ư ����
        // ��ư�� �� ������ �̿��� ó�� ��ư�� ��ġ ���
        float startX = -(notHeldChipList.Count - 1) * btnInterval / 2f;

        for (int i = 0; i < notHeldChipList.Count; i++)
        {
            //������ ���� ���� �� ������ ����
            InsertChipBtnPre.GetComponent<ChipBtnBase>().chipInfo = notHeldChipList[i];
            GameObject chipBtn = Instantiate(InsertChipBtnPre, Vector3.zero, Quaternion.identity, cheepMountedPanel.transform);

            notHeldChipBtnList.Add(chipBtn);//����Ʈ�� �߰�

            // ��ư�� ��ġ ���
            float posX = startX + i * btnInterval;

            // ��ư�� RectTransform �����ͼ� ��ġ ����
            RectTransform rectTransform = chipBtn.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, 0);
        }
    }

    //Ĩ ����â ������Ʈ �Լ� chipInfo: ������ Ĩ�� ����
    public void UpdateChipInfo(CheepInfo chipInfo)
    {
        if(chipInfo != null)
        {
            chipIconImage.gameObject.SetActive(true);
            chipIconImage.sprite = chipInfo.CheepIcon;//Ĩ���� ����
            chipName.text = chipInfo.CheepName;//Ĩ �̸�
            chipEffect.text = chipInfo.CheepEffectExplanationTxt;
        }
        else
            chipIconImage.gameObject.SetActive(false);
    }
}
