using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    [SerializeField] TowersDetails towersDetails;
    [SerializeField] EnemyDetails enemyDetails;
    public Tilemap spawnTile;
    public Dictionary<string, Queue<GameObject>> EnemyPoolDictionary;
    public Dictionary<string, Queue<GameObject>> TowerPoolDictionary;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        CleanObjectPoolList();
    }
    void CleanObjectPoolList()
    {
       for(int i=0;i< towersDetails.towerInfos.Count;i++)
        {
            if(towersDetails.towerInfos[i].IsDefaultTower == false)
            {
                towersDetails.towerInfos.RemoveAt(i);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        TowerPoolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (TowerInfo towerInfo in towersDetails.towerInfos)
            {
                //Create 10 gameObjects for each TowerBullet and save in TowerBulletPool
                Queue<GameObject> gameObjects = new Queue<GameObject>();
                for(int i =0; i < 10; i++)
                {
                GameObject bulletTower = Instantiate(towerInfo.BulletTower);
                bulletTower.SetActive(false);
                bulletTower.transform.position = new Vector3(-3f, 0.5f, 0f);
                //towerInfo.TowerBulletPool.Add(bulletTower);
                gameObjects.Enqueue(bulletTower);
                }
                TowerPoolDictionary.Add(towerInfo.towerName, gameObjects);
            }
            foreach (EnemyInfor enemyInfor in enemyDetails.enemyInfos)
            {
            //Create 10 gameObjects for each  EnemyBullet and save in EnemyBulletPool
            Queue<GameObject> gameObjects = new Queue<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                GameObject bulletEnemy = Instantiate(enemyInfor.BulletEnemy);
                bulletEnemy.SetActive(false);
                bulletEnemy.transform.position = new Vector3(-3f, 0.5f, 0f);
                gameObjects.Enqueue(bulletEnemy);
            }
            EnemyPoolDictionary.Add(enemyInfor.enemyName, gameObjects);
            
            }
    }
    public GameObject GetPooledObject(Queue<GameObject> objectPool)
    {
        return objectPool.Dequeue();
    }
    public void AddPooledObject(Queue<GameObject> objectPool, GameObject bullet)
    {
        bullet.transform.position = new Vector3(-3f, 0.5f, 0f);
        objectPool.Enqueue(bullet);
    }
    public void UpdateTowerDicTionary(TowerInfo towerInfo)
    {
        Queue<GameObject> gameObjects = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject bulletTower = Instantiate(towerInfo.BulletTower);
            bulletTower.SetActive(false);
            bulletTower.transform.position = new Vector3(-3f, 0.5f, 0f);
            //towerInfo.TowerBulletPool.Add(bulletTower);
            gameObjects.Enqueue(bulletTower);
        }
        TowerPoolDictionary.Add(towerInfo.towerName, gameObjects);
    }
}
