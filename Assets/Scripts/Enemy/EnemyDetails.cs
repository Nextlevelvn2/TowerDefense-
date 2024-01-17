using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails", menuName = "ScriptableObjects/EnemyDetails")]
public class EnemyDetails : ScriptableObject
{
    [SerializeField] public List<EnemyInfor> enemyInfos;
}
[System.Serializable]
public class EnemyInfor
{
    public string enemyName;
    public int enemyHealth;
    public float enemyMoveSpeed;
    public float RadiusAttact;
    public GameObject BulletEnemy;
    public int ammountDamage;
    public float bulletMoveSpeed;
    public float AttackInterval;
    public GameObject EnemyPrefab;
    public List<GameObject> BulletEnemyPool;
}
