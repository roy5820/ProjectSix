using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float speed = 60f;//발사 속도
    public int damage;//데미지
    bool isFire = true;//발사 여부
    public CharacterDirection fireDir = 0;//발사방향
    private Rigidbody2D rBody;//리지드 바디

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();//리지드바디 초기화
    }

    protected virtual void OnFire()
    {
        isFire = true;
    }

    protected virtual void FixedUpdate()
    {
        //발사 구현
        if (isFire && fireDir != 0)
        {
            Vector3 dir = new Vector3((int)fireDir, 0, 0) * speed;//발사 백터 계산
            rBody.velocity = dir;//총알 발사 구현
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//데미지 부여
        Destroy(this.gameObject);
    }
}
