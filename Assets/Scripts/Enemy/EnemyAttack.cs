using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class EnemyAttack : IEnemyState
{
    Collider2D[] colliders;
    EnemyInfor enemyChosen;
    private float attackInterval;
    private float nextAttack;
    private Animator animator;
    Transform enemyTransform;
    bool isMove;
    public void OnEnterState(EnemyManager enemyManager)
    {
        animator = enemyManager.anim;
        colliders = enemyManager.colliders;
        enemyChosen = enemyManager.enemyChosen;
        enemyTransform = enemyManager.transform;
        nextAttack = Time.time + 0.1f;
        //Define which enemyInfo would be accessed
        //enemyInfo = enemyDetails.enemyInfos;
        //foreach (EnemyInfor enemy in enemyInfo)
        //{
        //    if ((enemy.enemyName + "(Clone)") == enemyManager.gameObject.name)
        //    {
        //        enemyChosen = enemy;
        //        break;
        //    }
        //}
    }

    public void OnExitState(EnemyManager enemyManager)
    {
        animator.Play("Move");
    }

    public void OnUpdateState(EnemyManager enemyManager)
    {
        enemyManager.AttackArea();
        if (enemyManager.GetComponent<EnemyHealth>().IsDead())
        {
            enemyManager.ChangeState(enemyManager.enemyDie);
        }
        if (enemyManager.AttackArea()==false || isMove == true)
        {
            enemyManager.ChangeState(enemyManager.enemyMove);
        }
        else
        {
            AttackTower(enemyChosen);
        }
    }
    //public bool AttackArea()
    //{
    //    colliders = Physics2D.OverlapCircleAll(enemyTransform.position, enemyChosen.RadiusAttact, layerTower);
    //    AttackTower(enemyChosen);
    //    if (colliders.Length > 0) return true;
    //    else return false;
    //}
    private void AttackTower(EnemyInfor enemyInfolocal)
    {
        attackInterval = enemyInfolocal.AttackInterval;
        if (colliders.Length > 0)
        {
            if (Time.time > nextAttack - .8f)
            {
                animator.Play("Attack");
            }
            if (Time.time > nextAttack)
            {
                GameObject bullet = DataManager.Instance.EnemyPoolDictionary[enemyInfolocal.enemyName].Dequeue();
                bullet.transform.position = enemyTransform.position;
                bullet.GetComponent<Animator>().enabled = false;
                bullet.SetActive(true);
                //GameObject bullet = Instantiate(enemyInfolocal.BulletEnemy, gameObject.transform.position, Quaternion.identity);
                bullet.GetComponent<BulletEnemy>().SetTarget(colliders[0].transform, enemyInfolocal);
                nextAttack = Time.time + attackInterval;
                isMove = true;
                //Invoke("ChangeAnimation", 1f);
            }
        }
    }
}
