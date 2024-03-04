using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수

    private void Start()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화
    }
}
