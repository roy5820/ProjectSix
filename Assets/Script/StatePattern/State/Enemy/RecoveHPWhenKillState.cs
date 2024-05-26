using System.Collections;
using UnityEngine;

//Enemy  객체가 죽을 시 플레이어의 체력 회복
public class RecoveHPWhenKillState : DieState
{
    public int recoveryLevel = 75;//체력 회복 수치
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //플레이어 체력 회복
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");//플레이어 오브젝트 가져오기
        if(playerObj != null)
        {
            CharacterController playerController = playerObj.GetComponent<CharacterController>();//캐릭터 컨트롤러 가져오기

            playerController.NowHp += recoveryLevel;//체력 회복
        }
        

        yield return base.StateFuntion(datas);
    }
}