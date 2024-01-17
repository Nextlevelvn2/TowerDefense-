using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public int Health;
    int maxHealth;
    public int gainAmount;
    Tilemap spawnTilemap;
    private List<TowerInfo> towerInfo;
    [SerializeField] Slider HealthSlider;
    // Start is called before the first frame update
    private void Start()
    {
        spawnTilemap = DataManager.Instance.spawnTile;
        towerInfo = GetComponent<TowerManager>().towersDetails.towerInfos;
        foreach(TowerInfo tower in towerInfo)
        {
            if(tower.towerName == gameObject.name)
            {
                Health = tower.towerHealth;
                maxHealth = tower.towerHealth;
                break;
            }
        }
    }
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            ResetSpawnCell();
            for (int i = TowerUIManager.Instance.towerList.Count - 1; i >= 0; i--)
            {
                if (TowerUIManager.Instance.towerList[i] == gameObject)
                {
                    TowerUIManager.Instance.towerList.RemoveAt(i);
                }
            }
            //Enable Boxcollider
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
        UpdateHealthBar(Health, maxHealth);
    }
    //Reset the collider type of Spawn tile
    void ResetSpawnCell()
    {
        var cellPos = spawnTilemap.WorldToCell(gameObject.transform.position);
        spawnTilemap.SetColliderType(cellPos, Tile.ColliderType.Sprite);
    }
    public void UpgradeTower(int amount)
    {
        Health = Health * amount;
    }
    void UpdateHealthBar(int currentValue, int maxValue)
    {
        HealthSlider.value = currentValue / maxValue;
    }
}
