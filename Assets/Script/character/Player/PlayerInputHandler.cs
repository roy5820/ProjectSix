using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private GameManager _gameManager;//게임 메니저
    private PlayerController _playerController;//플레이어 컨트롤러

    //입력 키 값
    [Header("Input Key Seting")]
    public KeyCode leftMoveKey;
    public KeyCode rightMoveKey;
    public KeyCode turnAboutKey;

    private void Start()
    {
        _gameManager = GameManager.Instance;//게임 메니저 가져오기
        _playerController = this.GetComponent<PlayerController>();//플레이어 컨트롤러 초기화
    }

    private void Update()
    {
        //플레이어 턴 체크
        if (_playerController.isTurnReady)
        {
            //이동 키 입력 처리(이동 키 하나로 이동 + 일반 공격 처리)
            if (Input.GetKeyDown(leftMoveKey) || Input.GetKeyDown(rightMoveKey))
            {
                CharacterDirection moveDir = Input.GetKeyDown(leftMoveKey) ? CharacterDirection.Left : CharacterDirection.Right;//캐릭터 이동 방향
                int onIndex = _gameManager.GetPlatformIndexForObj(this.gameObject);
                int nexIndex = onIndex + ((int)moveDir);//이동할 플렛폼 index
                //이동 + 공격 여부 체크
                if (nexIndex >= 0 && nexIndex < _gameManager.PlatformList.Length)
                {
                    //이동가능 여부 체크
                    if (_gameManager.GetOnPlatformObj(nexIndex) == null)
                        _playerController.TransitionState("Move", moveDir);
                    //아닐 시 공격 실행
                    else if(moveDir == _playerController.direction)
                        _playerController.TransitionState("NormalAttack", 1.0f);
                }
            }
            //턴 전환 키
            if (Input.GetKeyDown(turnAboutKey))
            {
                _playerController.TransitionState("Turnabout");
            }
        }
    }
}
