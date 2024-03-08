using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터 상태 값 appears: 등장, normal: 일반상태, invicible: 무적, attack: 공격 중, death: 죽음상태
public enum CharacterState
{
    appears, normal, invincible, attack, death
}

public class CharacterBase : MonoBehaviour, CharacterStateInterface
{
    GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수
    GameState gameState;//현재 게임 상태를 저장 하는 변수

    //캐릭터가 행동할 작업에 대한 정보
    [System.Serializable]
    public class ActionInfo
    {
        public string actionName;//작업명
        public ActionBase action;//행동 컴포넌트
        //생성자
        public ActionInfo(string actionName, ActionBase action)
        {
            this.actionName = actionName;
            this.action = action;
        }
    }
    public List<ActionInfo> actionList = new List<ActionInfo>();//행동 리스트 선언

    public CharacterState state;//캐릭터 상태 값
    public int maxHp = 100;//최대체력
    private int nowHp = 100;//현제체력
    public int maxShild = 20;//최대 보호막 량
    private int nowShild = 20;//현재 보호막 량

    private void Start()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화
    }

    //캐릭터 피격 처리
    public void Hit()
    {

    }
}
