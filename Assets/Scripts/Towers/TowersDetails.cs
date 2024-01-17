using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "towerDetails", menuName = "ScriptableObjects/towerDetails")]
public class TowersDetails : ScriptableObject
{
    public List<Sprite> spritesList;
    public List<TowerInfo> towerInfos;
}
[System.Serializable]
public class TowerInfo
{
    public string towerName;
    public int towerHealth;
    public float RadiusAttact;
    public GameObject BulletTower;
    public int ammountDamage;
    public float bulletMoveSpeed;
    public float AttackInterval;
    public int towerPrice;
    public Sprite towerSprite;
    public Sprite towerSpriteUpdated;
    public int towerUpdatePower;
    public int towerUpdatePrice;
    public GameObject TowerPrefab;
    public bool IsDefaultTower;
} 