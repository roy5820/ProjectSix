using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
//�÷��̾� ����
public class TrackPlayerState : StateBase
{

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //�÷��̾� ���� ����
        int targetIndex = _battleManager.GetPlatformIndexForObj(GameObject.FindGameObjectWithTag("Player"));//�÷��̾� ��ġ ��������
        int thisIndex = _battleManager.GetPlatformIndexForObj(characterController.gameObject);//�ش� ��ü�� ��ġ ��������
        
        CharacterDirection targetDir = targetIndex < thisIndex ? CharacterDirection.Left : CharacterDirection.Right ;//Ÿ�� ����
        CharacterDirection thisDir = characterController.Direction;//�ش� ��ü�� �ٶ󺸴� ����
        
        //�ٷκ��� ���⿡ Ÿ���� ������ ����
        if (targetDir == thisDir)
            characterController.TransitionState(StateEnum.Move, targetDir);//�̵� ���� ����
        //�ƴ� ��� ���� ��ȯ ����
        else
            characterController.TransitionState(StateEnum.Turnabout);//������ȯ ���� ����
        yield return null;
    }
}
