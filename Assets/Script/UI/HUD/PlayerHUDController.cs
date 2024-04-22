using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    private CharacterController _characterController;//배틀매니저
    public Image playerHpHUD;//플레이어 체력바 

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();//캐릭터 매니저 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if ((_characterController))
        {
            float maxHp = _characterController.maxHp;
            float nowHp = _characterController.NowHp;
            playerHpHUD.fillAmount = (nowHp / maxHp);
        }
        
    }
}
