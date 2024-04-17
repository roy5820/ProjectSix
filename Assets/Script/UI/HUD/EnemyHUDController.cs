using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDController : MonoBehaviour
{
    EnemyController _eController;//적 컨트롤러
    public Image hpBar;//체력바 이미지 오브젝트

    private void Start()
    {
        _eController = transform.parent.GetComponent<EnemyController>();//적 컨트롤러 가져오기
    }

    private void Update()
    {
        
        //상태 값에 따른 HUD 없데이트
        if (_eController)
        {
            if (_eController.direction == CharacterDirection.Left)
                this.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            UpdateHealthBar(_eController.NowHp, _eController.maxHp);
        }
    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        hpBar.fillAmount = healthPercentage;
    }
}
