using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ĩ�κ��丮�� �ִ� Ĩ������ �������� Ĩ����� �����ϴ� Ŭ����
public class SetCheepForCharacter : MonoBehaviour
{
    GameManager _gameManager = null;//���� �޴���
    CharacterController _chracterController = null;//ĳ���� ��Ʈ�ѷ�
    
    public List<CheepPair> cheepList = new List<CheepPair>();//Ĩ Ÿ�԰� ��� ���� ������Ʈ�� �����ϴ� ����Ʈ
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//���� �޴��� �� �ʱ�ȭ
        _chracterController = GetComponent<CharacterController>();//ĳ���� ��Ʈ�ѷ� �� �ʱ�ȭ

        //nullüũ
        if(_gameManager != null && _chracterController != null)
        {
            //Ĩ �κ��丮�� Ĩ ������ �������� ���� Ÿ�Ժ� ���� ����
            List<CheepInfo> cheepList = _gameManager.cheepDataBase;//Ĩ���� ��������
            List<int> haveCheepIDS = _gameManager.cheepInventory;//�������� Ĩ ID��������
            //Ĩ�κ��丮�� �ִ� Ĩ���� ID�� �������� Ĩ ��ɱ��� �ϴ� CHeepBase��ü�� ã�� ����
            for(int i = 0; i < haveCheepIDS.Count; i++)
            {
                
                CheepInfo cheepInfo = cheepList.Find(cheep => cheep.CheepID.Equals(haveCheepIDS[i]));
                if(cheepInfo != null )
                {
                    CheepType cheepType = cheepList.Find(cheep => cheep.CheepID.Equals(haveCheepIDS[i])).cheepType;
                    TransitionCheep(cheepType);
                }
            }

        }
    }

    //Ĩ Ÿ�Ժ��� Ĩ ��� ���� �Լ� ȣ��
    public void TransitionCheep(CheepType cheepType)
    {
        CheepBase cheepComponent = cheepList.Find(cheep => cheep.cheepType.Equals(cheepType)).cheepComponent;//Ĩ Ÿ�Կ� ���� ��� ���� ������Ʈ ��������
        //nullüũ �� Ĩ ����
        if (cheepComponent != null)
            cheepComponent.ActivateChipEffect();
    }

}
