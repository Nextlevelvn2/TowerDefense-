using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    private Transform target;
    private float bulletSpeed;
    private int amountDamage;
    private string bulletName;
    private Animator anim;
    [SerializeField] LayerMask layerEnemy;
    Collider2D[] colliders;
    private float RadiusAttackForDefender = 2f;
    TowerInfo towerInfo1;
    //Sound 
    [SerializeField] AudioClip arrowAttackclip, bombAttackClip, defenderClip;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if(bulletName != "Defender")
        {
            if (!target)
            {
                return;
            }
            else
            {
                FollowEnenmy();
            }
        }
        else if(bulletName == "Defender")
        {
            AttackArea();
            if (target != null)
            {
                FollowEnenmy();
            }
            else
            {
                DefenderStopMove();
            }
        }
    }
    #region Detect Enemy for defenderBullet
    void AttackArea()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, RadiusAttackForDefender, layerEnemy);
        AttackEnemy();
    }
    void AttackEnemy()
    {
        if (colliders.Length > 0)
        {
            target = colliders[0].transform;
        }
    }
    #endregion
    void DefenderStopMove()
    {
        rb.velocity = new Vector2(0f, 0f);
    }
    public void FollowEnenmy()
    {
        if(bulletName == "Defender")
        {
            //make defender face targetedEnemy
            if (target.position.x < gameObject.transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            //Follow Enemy
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;

        }
        else
        {
            PlaySoundWeapon();
            Vector3 vectorTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorTarget.y, vectorTarget.x) * Mathf.Rad2Deg - 180f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 50f);
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
            Invoke("DeactiveBullet", 5f);
        }
    }
    void DeactiveBullet()
    {
        //DataManager.Instance.AddPooledObject(towerInfo1.TowerBulletPool, gameObject);
        DataManager.Instance.TowerPoolDictionary[towerInfo1.towerName].Enqueue(gameObject);
    }
    public void SetTarget(Transform _target, TowerInfo towerInfo)
    {
        towerInfo1 = towerInfo;
        bulletSpeed = towerInfo.bulletMoveSpeed;
        amountDamage = towerInfo.ammountDamage;
        bulletName = towerInfo.towerName;
        RadiusAttackForDefender = towerInfo.RadiusAttact;
        if (bulletName != "Defender")
        {
            target = _target;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bulletName == "Defender")
        {
            if (collision.tag == "Enemy")
            {
                anim = GetComponent<Animator>();
                anim.Play("DefenderAttackAnimation");
                PlaySoundAttack(defenderClip);
                if (collision.GetComponent<EnemyHealth>().Health > 0)
                {
                    collision.GetComponent<EnemyHealth>().TakeDamage(amountDamage);
                }
                Destroy(gameObject, 1f);
            }
                
        }
        else
        {
            if (collision.tag == "Enemy")
            {
                if (collision.GetComponent<EnemyHealth>().Health > 0)
                {
                    collision.GetComponent<EnemyHealth>().TakeDamage(amountDamage);
                }
                gameObject.SetActive(false);
                DataManager.Instance.TowerPoolDictionary[towerInfo1.towerName].Enqueue(gameObject);
            }
        }
    }
    void PlaySoundAttack(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    void PlaySoundWeapon()
    {
        if(gameObject.name == "BombTower(Clone)")
        {
            PlaySoundAttack(bombAttackClip);
        }
        else
        {
            PlaySoundAttack(arrowAttackclip);
        }
    }
}
