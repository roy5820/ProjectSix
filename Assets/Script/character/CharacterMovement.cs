using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D characterRbody = null;//캐릭터 리지드 바디
    private Collider2D characterCol2D = null;//캐릭터 콜라이더
    private GameManager getGameManager = null;//게임 메니저

    public Coroutine moveCoroutine = null;//이동 구현 코루틴 작동 시 저장할 변수

    public LayerMask platformLayer;//플렛폼 레이어 값

    private void Start()
    {
        characterRbody = GetComponent<Rigidbody2D>();//리지드 바디 값 초기화
        characterCol2D = GetComponent<Collider2D>();//콜라이더 값 초기화
        getGameManager = GameManager.Instance;//게임 메니저 값 초기화
    }

    //타일 간 이동 구현 코루틴 moveDirX: 이동방향, movePower: 이동속도, moveSpaceDistance:이동거리(칸)
    public IEnumerator StraightLineMovement(int moveDirX, float movePower, int moveSpaceDistance)
    {
        int onPlatformIndex = -1;//플렛폼 인덱스 값이 저장될 변수
        //moveSaceNum만큼 칸 이동을 구현하는 반복문
        for (int i = 0; i < moveSpaceDistance; i++)
        {
            onPlatformIndex = getGameManager.GetPlatformIndexForObj(gameObject);//자신이 속한 플렛폼 인덱스 값
            int moveIndex = onPlatformIndex + moveDirX;//이동 할 플렛폼의 인데스 값
            
            Vector3 targetPlatformPos = getGameManager.GetStandingPos(moveIndex);//이동할 플렛폼 오브젝트 값
            
            GameObject targetPlatformOnObj = getGameManager.GetOnPlatformObj(moveIndex);//플렛폼 정보값 초기화

            //이동 가능 여부 체크
            if (targetPlatformPos != Vector3.zero ? targetPlatformOnObj == null : false)
            {
                //실질적인 이동구현을 할 반복문
                while (moveDirX > 0 ? transform.position.x < targetPlatformPos.x : transform.position.x > targetPlatformPos.x)
                {
                    characterRbody.velocity = new Vector2(moveDirX * movePower, 0);//이동 구현
                    yield return null;
                }
                characterRbody.velocity = Vector3.zero;//이동 종료시 이동 값 zero로 초기화
                transform.position = new Vector3(targetPlatformPos.x, transform.position.y, transform.position.z);// 이동 후 위치값 조정
            }
            //이동 불가 시 반복문 강제 종료
            else
                break; 
        }
        
        moveCoroutine = null;//코루틴 종료
    }
}