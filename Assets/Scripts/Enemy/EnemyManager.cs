using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform enemyTransform;
    public IEnemyState currentState;
    public EnemyMove enemyMove = new EnemyMove();
    public EnemyAttack enemyAttack = new EnemyAttack();
    public EnemyDie enemyDie = new EnemyDie();
    //Field
    //float moveSpeed = .5f;
    [SerializeField] EnemyDetails enemyDetails;
    [HideInInspector]
    public EnemyInfor enemyChosen;
    [HideInInspector]
    public Collider2D[] colliders;
    [SerializeField] LayerMask layerTower;
    [SerializeField]public Animator anim;
    public bool isDead;

    private void Start()
    {
        isDead = GetComponent<EnemyHealth>().IsDead();
        //Check for details of enemy
        foreach (EnemyInfor enemy in enemyDetails.enemyInfos)
        {
            if ((enemy.enemyName + "(Clone)") == gameObject.name)
            {
                enemyChosen = enemy;
                break;
            }
        }
        enemyTransform = transform;
        ChangeState(enemyMove);
    }
    // Exit previous state and enter new state
    public void ChangeState(IEnemyState newState)
    {
        if(currentState!= null)
        {
            currentState.OnExitState(this);
        }
        currentState = newState;
        currentState.OnEnterState(this);
    }
    //check for new state to be updated( call the OnStateUpdate method in members IEnemyState)
    private void Update()
    {
        currentState.OnUpdateState(this);
    }
    public bool AttackArea()
    {
        colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, enemyChosen.RadiusAttact, layerTower);
        if (colliders.Length > 0) return true;
        else return false;
    }
    public void DeletEnemy()
    {
        Destroy(gameObject,1f);
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    //Random Path According To CurrentLevel
}
