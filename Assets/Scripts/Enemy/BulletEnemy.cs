using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private Transform target;
    private float bulletSpeed;
    private int amountDamage;
    private Animator anim;
    EnemyInfor enemyInfor1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)
        {
            return;
        }
        else
        {
            FollowTower();
        }
    }
    public void FollowTower()
    {
        Vector3 vectorTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorTarget.y, vectorTarget.x) * Mathf.Rad2Deg - 180f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 50f);
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
        Invoke("DeactivateBulletEnemy", 5f);
    }
    public void SetTarget(Transform _target, EnemyInfor enemyInfor)
    {
        target = _target;
        bulletSpeed = enemyInfor.bulletMoveSpeed;
        amountDamage = enemyInfor.ammountDamage;
        enemyInfor1 = enemyInfor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            rb.velocity = new Vector2(0f, 0f);
            anim.enabled = true;
            collision.GetComponent<TowerHealth>().TakeDamage(amountDamage);
            Invoke("DeactivateBulletEnemy",.2f);
        }
    }
    void DeactivateBulletEnemy()
    {
        gameObject.transform.position = new Vector3(-3f, 0.5f, 0f);
        gameObject.SetActive(false);
        //DataManager.Instance.AddPooledObject(enemyInfor1.BulletEnemyPool, gameObject);
        DataManager.Instance.EnemyPoolDictionary[enemyInfor1.enemyName].Enqueue(gameObject);
    }
}
