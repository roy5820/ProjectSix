using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxationSystem : MonoBehaviour
{
    public int recoveryHp;//회복 체력
    private GameManager _gameManager;//게임 메니저
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임 메니저 값 초기화

        //체력 회복 구현
        if(_gameManager.playerStatus != null)
        {
            _gameManager.playerStatus.nowHp += recoveryHp;
            if (_gameManager.playerStatus.nowHp > _gameManager.playerStatus.maxHp)
                _gameManager.playerStatus.nowHp = _gameManager.playerStatus.maxHp;
        }
        
    }
}
