using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Enemies;
    private int amountSpawn;
    private int maxamountSpawn =3;
    private float timeWaveSpawn;
    private int spawncount = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeWaveSpawn = 1f;
        amountSpawn = 0;
        Invoke("SpawnEnemy", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void SpawnEnemy()
    {
            StartCoroutine(StartSpawnEnemy());
    }
    IEnumerator StartSpawnEnemy()
    {
        GameObject obj = Instantiate(Enemies[Random.Range(0, Enemies.Count)], transform);
        obj.transform.position = transform.position;
        obj.SetActive(true);
        //obj.GetComponent<EnemyMoveState>().Init();
        amountSpawn += 1;
        if (amountSpawn == maxamountSpawn)
        {
            amountSpawn = 0;
            maxamountSpawn += 1;
            timeWaveSpawn = 15f;
        }
        else if (amountSpawn < maxamountSpawn)
        {
            timeWaveSpawn = 1f;
        }
        yield return new WaitForSeconds(timeWaveSpawn);
        StartCoroutine(StartSpawnEnemy());
    }
}
