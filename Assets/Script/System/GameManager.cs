using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;//게임 메니저 인스턴스화를 위한 변수 값
    public enum State
    {
        start, playerTurn, eneyTurn, lose, win, rest
    }

    public State state;//게임 상태 값이 저장되는 변수


    public GameObject[] PlatformList;//플랫폼 리스트

    private void Awake()
    {
        state = State.start;//게임 시작 시 start상태값으로 초기화

        //인스턴스 초기화
        if(instance == null)
            instance = this;
    }



    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
}