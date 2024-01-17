using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="TowerShopDetails", menuName = "ScriptableObjects/TowerShopDetails")]
public class TowerShopDetails : ScriptableObject
{
    public List<TowerShopTemPlate> towerShopTemplates = new List<TowerShopTemPlate>();
}
[System.Serializable]
public class TowerShopTemPlate
{
    public TowerInfo towerInfo = new TowerInfo();
    public int towerPrice;
    public string itemDescription;
}
