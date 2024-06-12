using System.Collections;
using UnityEngine;

//ĳ���� ����
public class DieState : StateBase
{
    public GameObject effectList = null;//����Ʈ ���� �����Ǵ� ��
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //����Ʈ ��Ȱ��ȭ
        if(effectList)
            effectList.SetActive(false);
        yield return new WaitForSeconds(sateDelayTime);//�ִϸ��̼� ����� ���� ������
        //ĳ���� ���� ��� ����
        Debug.Log(gameObject.name + " is Die");
        _battleManager.onEnemysList.Remove(characterController.gameObject);
        Destroy(characterController.gameObject);

        yield return null;
    }
}