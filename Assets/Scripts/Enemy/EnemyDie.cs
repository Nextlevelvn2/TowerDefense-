using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : IEnemyState
{
    public int Health;
    public int gainAmount;
    public List<Collider2D> collisionsList = new List<Collider2D>();
    Animator animator;
    public void OnEnterState(EnemyManager enemyManager)
    {
        animator = enemyManager.anim;
    }

    public void OnExitState(EnemyManager enemyManager)
    {
        
    }

    public void OnUpdateState(EnemyManager enemyManager)
    {
        animator.Play("Die");
        enemyManager.DeletEnemy();
    }
}
