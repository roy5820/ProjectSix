using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D characterRbody = null;//캐릭터 리지드 바디
    private Collider2D characterCol2D = null;//캐릭터 콜라이더
    private GameManager getGameManager = null;//게임 메니저

    Coroutine moveCoroutine = null;//이동 구현 코루틴 작동 시 저장할 변수

    private void Start()
    {
        characterRbody = GetComponent<Rigidbody2D>();//리지드 바디 값 초기화
        characterCol2D = GetComponent<Collider2D>();//콜라이더 값 초기화
        getGameManager = GameManager.Instance;//게임 메니저 값 초기화
    }

    private void Update()
    {
        //이동 테스트를 위한 키입력
        float getKeyH = Input.GetAxis("Horizontal");//키입력 받기

        if (getKeyH != 0)
            Debug.Log("이동중 여부: " + moveCoroutine != null);

        if (getKeyH != 0 && moveCoroutine == null)
        {
            Debug.Log("이동 키 입력: " + getKeyH);
            moveCoroutine = StartCoroutine(StraightLineMovement(getKeyH, 60, 1));
        }
    }

    //이동 구현 코루틴
    public IEnumerator StraightLineMovement(float moveDirX, float movePower, int moveSpaceDistance)
    {
        PlatformInfoManagement parentPlatformInfo = null;//플렛폼 정보값을 가져올 변수
        int platformIndex = -1;//플렛폼 인덱스 값이 저장될 변수
        //moveSaceNum만큼 칸 이동을 구현하는 반복문
        for (int i = 0; i < moveSpaceDistance; i++)
        {
            parentPlatformInfo = transform.parent.GetComponent<PlatformInfoManagement>();//자신이 서있는 플렛폼의 정보 컴포넌트를 가져오기
            platformIndex = parentPlatformInfo.indexNum;//자신이 속한 플렛폼 인덱스 값
            int moveIndex = platformIndex + (moveDirX > 0 ? 1 : -1);//이동 할 플렛폼의 인데스 값
            Debug.Log("인덱스 값: "+platformIndex + ", " + moveIndex);

            Vector3 targetPlatformPos = getGameManager.GetStandingPos(moveIndex);//이동할 플렛폼 오브젝트 값
            GameObject targetPlatformOnObj = getGameManager.GetOnPlatformObj(moveIndex);//플렛폼 정보값 초기화

            //이동 가능 여부 체크
            if (targetPlatformPos != Vector3.zero ? targetPlatformOnObj == null : false)
            {
                Debug.Log("예외처리 성공: " + transform.position.x + ", " + targetPlatformPos.x);
                characterCol2D.enabled = false;//이동 시 콜라이더 비활성화

                //실질적인 이동구현을 할 반복문
                while (moveDirX > 0 ? transform.position.x < targetPlatformPos.x : transform.position.x > targetPlatformPos.x)
                {
                    characterRbody.velocity = new Vector2(moveDirX * movePower, 0);//이동 구현
                    Debug.Log(characterRbody.velocity);
                    yield return null;
                }
                characterRbody.velocity = Vector3.zero;//이동 종료시 이동 값 zero로 초기화
                characterCol2D.enabled = false;//이동 종료 시 콜라이더 활성화
            }
            //이동 불가 시 반복문 강제 종료
            else
                break;
        }
        
        moveCoroutine = null;//코루틴 종료
        Debug.Log("코루틴 종료: " + (moveCoroutine == null));
        yield return null;
    }
}