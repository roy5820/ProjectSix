using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBase
{
    public float fireTime = 5f;//레이저 발사 시간
    private float nowTime = 0;
    public GameObject LaserLight;//레이저 라이트

    protected override void FixedUpdate()
    {
        //발사 시간이 지나면 레이저 삭제
        if(isFire && nowTime >= fireTime)
            Destroy(this.gameObject);

        if (isFire && fireDir != 0)
        {
            nowTime += Time.deltaTime;
        }
    }

    public override void OnFire()
    {
        
        LaserLight.SetActive(true);
        base.OnFire();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//데미지 부여
    }
}
