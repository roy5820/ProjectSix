using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour, CharacterStateInterface
{
    GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수
    State gameState;//현재 게임 상태를 저장 하는 변수

    private void Start()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화
    }
}
