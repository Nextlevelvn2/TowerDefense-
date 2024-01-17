using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : IEnemyState
{
    //Field
    //MOVESTATE
    //MoveSpeed
    float moveSpeed;
    //Animation
    Animator animator;
    Transform target;
    Transform enemyTransform;
    int pathIndex = 0;
    private int numberPath;
    int currentLevel =1;
    private float attackInterval =2f;
    private float nextAttack;
    //PathToFollow
    Transform[] pathToFollow;
    public void OnEnterState(EnemyManager enemyManager)
    {
        if(pathIndex == 0)
        {
            pathToFollow = PathManager.Instance.PathEnemy[numberPath];
        } 

        //change the pathIndex as player pathIndex = pathIndex-1 if it enter AttackState and then ReEnter MoveState
        animator = enemyManager.anim;
        enemyTransform = enemyManager.enemyTransform;
        numberPath = DefinePath(currentLevel);
        target = pathToFollow[pathIndex];

        //Force Enemies to follow path
        animator.Play("Move");
        moveSpeed = enemyManager.enemyChosen.enemyMoveSpeed;
        nextAttack = Time.time + attackInterval;
    }

    public void OnExitState(EnemyManager enemyManager)
    {
        //Move
    }

    public void OnUpdateState(EnemyManager enemyManager)
    {
        if (enemyManager.GetComponent<EnemyHealth>().IsDead())
        {
            enemyManager.ChangeState(enemyManager.enemyDie);
        }
        if (enemyManager.AttackArea() && nextAttack<Time.time)
        {
            enemyManager.ChangeState(enemyManager.enemyAttack);
        }
        else
        {
            EnemyFollowPath(enemyManager);
        }
    }
    void EnemyFollowPath(EnemyManager enemyManager)
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target.position, Time.deltaTime * moveSpeed);
        //Rotate Enemy toward path
        float scalex = enemyManager.gameObject.transform.localScale.x;
        Vector3 scale = enemyManager.gameObject.transform.localScale;
        if (enemyManager.gameObject.transform.position.x < pathToFollow[pathIndex].position.x)
        {
            scale.x = Mathf.Abs(scalex);
        }
        else
        {
            scale.x = -Mathf.Abs(scalex);
        }
        enemyManager.gameObject.transform.localScale = scale;

        //Increase the path index
        if (Vector2.Distance(enemyTransform.position, target.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == pathToFollow.Length)
            {
                //enemyManager.ChangeState(enemyManager.enemyDie);
                enemyManager.DestroyEnemy();
                LifeManager.Instance.LoseLife();
            }
            else
            {
                target = pathToFollow[pathIndex];
            }
        }
    }
    public int DefinePath(int currentLevel)
    {
        int result;
        switch (currentLevel)
        {
            case 1:
                result = Random.Range(0, 2);
                break;
            case 2:
                result = Random.Range(2, 5);
                break;
            default:
                result = -1;
                break;
        };
        return result;
    }
}
