using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackState : MonoBehaviour
{
    [SerializeField] LayerMask layerEnemy;
    Collider2D[] colliders;
    [SerializeField] private float RadiusAttack;
    private List<TowerInfo> towerInfo = new List<TowerInfo>();
    TowerInfo towerChosen;
    private float attackInterval;
    private float nextAttack;

    private void Start()
    {
        towerInfo = gameObject.GetComponent<TowerManager>().towersDetails.towerInfos;
        nextAttack = Time.time + 0.1f;
        //Set fields values
        foreach (TowerInfo tower in towerInfo)
        {
            if (tower.towerName == gameObject.name)
            {
                RadiusAttack = tower.RadiusAttact;
                towerChosen = tower;
                attackInterval = towerChosen.AttackInterval;
                break;
            }
        }
    }
    private void Update()
    {
        AttackArea();
    }
    void AttackArea()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, RadiusAttack, layerEnemy);
        AttackEnemy(towerChosen);
    }
    void AttackEnemy(TowerInfo towerInfo)
    {
        if (colliders.Length>0)
        {
            if (Time.time > nextAttack)
            {
                if(gameObject.name != "Defender")
                {
                    //GameObject bullet = DataManager.Instance.GetPooledObject(towerInfo.TowerBulletPool);
                    GameObject bullet = DataManager.Instance.TowerPoolDictionary[towerInfo.towerName].Dequeue();
                    bullet.transform.position = gameObject.transform.position;
                    bullet.SetActive(true);
                    bullet.GetComponent<BulletManager>().SetTarget(colliders[0].transform, towerChosen);
                    nextAttack = Time.time + attackInterval;
                }
                else
                {
                    GameObject bullet = Instantiate(towerInfo.BulletTower);
                    bullet.transform.position = gameObject.transform.position;
                    bullet.SetActive(true);
                    bullet.GetComponent<BulletManager>().SetTarget(colliders[0].transform, towerChosen);
                    nextAttack = Time.time + attackInterval;
                }
            }
        }
    }
    public void UpgradeTower(int amount)
    {
        attackInterval /=(2*amount);
    }
}
