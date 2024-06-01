using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float speed = 60f;//�߻� �ӵ�
    public int damage;//������
    protected bool isFire = true;//�߻� ����
    public CharacterDirection fireDir = 0;//�߻����
    private Rigidbody2D rBody;//������ �ٵ�

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();//������ٵ� �ʱ�ȭ
    }

    public virtual void OnFire()
    {
        isFire = true;
    }

    protected virtual void FixedUpdate()
    {
        //�߻� ����
        if (isFire && fireDir != 0)
        {
            Vector3 dir = new Vector3((int)fireDir, 0, 0) * speed;//�߻� ���� ���
            rBody.velocity = dir;//�Ѿ� �߻� ����
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//������ �ο�
        Destroy(this.gameObject);
    }
}
