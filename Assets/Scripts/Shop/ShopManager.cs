using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject towerTemplatPrefab;
    [SerializeField] TowerShopDetails towerShopDetails;
    [SerializeField] GameObject ContentShop;
    private void Start()
    {
        Debug.Log(towerShopDetails.towerShopTemplates.Count);
        for(int i=0; i < towerShopDetails.towerShopTemplates.Count; i++)
        {
            GameObject towerShop = Instantiate(towerTemplatPrefab, towerTemplatPrefab.transform.parent);
            towerShop.SetActive(true);
            towerShop.GetComponent<ItemTemplate>().towerShopTemPlate = towerShopDetails.towerShopTemplates[i];
        }
    }
}

