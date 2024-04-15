using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
//플레이어 추적
public class TrackPlayerState : StateBase
{

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //플레이어 추적 구현
        int targetIndex = _gameManager.GetPlatformIndexForObj(GameObject.FindGameObjectWithTag("Player"));//플레이어 위치 가져오기
        int thisIndex = _gameManager.GetPlatformIndexForObj(gameObject);//해당 객체의 위치 가져오기
        
        CharacterDirection targetDir = targetIndex < thisIndex ? CharacterDirection.Left : CharacterDirection.Right ;//타겟 방향
        CharacterDirection thisDir = characterController.direction;//해당 객체의 바라보는 방향
        //바로보는 방향에 타겟이 있으면 전진
        if (targetDir == thisDir)
            characterController.TransitionState(StateEnum.Move, targetDir);//이동 상태 실행
        //아닌 경우 방향 전환 실행
        else
            characterController.TransitionState(StateEnum.Turnabout);//방향전환 상태 실행

        yield return base.StateFuntion(datas);
    }
}
