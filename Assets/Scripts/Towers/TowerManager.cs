using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    public List<GameObject> towerList;
    // Start is called before the first frame update
    public Vector3 towerPosition;
    public TowersDetails towersDetails;
    private bool canUpgrade = false;
    [SerializeField] TextMeshPro priceUpgrade;
    [SerializeField] private GameObject upgradePanel;
    private int priceforUpgrade;
    [SerializeField] GameObject DustFX;
    //tower member in towerList 
    GameObject _obj;
    //Variable to check if it is possible to remove _obj from list(Check if it is already upgraded or not)
    bool canRemove_obj = false;
    void Start()
    {
        Invoke("ChangeTowerSprite", 0.001f);
        towerList.Add(gameObject);
    }
    private void ChangeTowerSprite()
    {
        upgradePanel.SetActive(true);
        List<TowerInfo> towerInfos = new List<TowerInfo>();
        towerInfos = towersDetails.towerInfos;
        foreach(TowerInfo tower in towerInfos)
        {
            if(tower.towerName == gameObject.name)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = tower.towerSprite;
                priceUpgrade.text = tower.towerUpdatePrice.ToString();
                priceforUpgrade = tower.towerUpdatePrice;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForUpgrade();
    }
    void CheckForUpgrade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag== "upgradeBtn")
                {
                    foreach(GameObject obj in TowerUIManager.Instance.towerList)
                    {
                        //check for position of the tower in towerlist to know which tower will be upgraded
                        if(hit.collider.gameObject.GetComponent<SaveTowerPos>().towerPos == obj.GetComponent<TowerManager>().towerPosition)
                        {
                            _obj = obj;
                            //foreach(TowerInfo towerInfo in towersDetails.towerInfos)
                            //{
                            //    //check for the name saved in the SaveTowerPos to know which TowerInfo will be applied to the upgraded tower
                            //    if(towerInfo.towerName == hit.collider.gameObject.GetComponent<SaveTowerPos>().towerName)
                            //    {
                            //        towerChosen = towerInfo;
                            //    }
                            //}
                            UpGradeTower(obj);
                        }
                    }
                    //Remove Obj from listTower
                    for (int i = TowerUIManager.Instance.towerList.Count - 1; i >= 0; i--)
                    {
                        if (TowerUIManager.Instance.towerList[i] == _obj && canRemove_obj == true)
                        {
                            TowerUIManager.Instance.towerList.RemoveAt(i);
                            canRemove_obj = false;
                        }
                    }

                }
            }
        }
    }
    void UpGradeTower(GameObject obj)
    {
        //Check if it is enough money to upgrade
        if (CurrencyManager.Instance.CheckToUseCurrency(priceforUpgrade))
        {
            //use reward VFX
            obj.GetComponent<TowerManager>().DustFX.SetActive(false);
            obj.GetComponent<TowerManager>().DustFX.SetActive(true);
            //play Sound
            gameObject.GetComponent<PlaySoundOnEnable>().PlaySoundUpgrade();
            //change sprite to the saved sprite in towerInfoInSaveTowerPos
            SaveTowerPos saveTowerPos = obj.transform.GetChild(0).GetChild(0).GetComponent<SaveTowerPos>();
            obj.GetComponent<SpriteRenderer>().sprite = saveTowerPos.towerInfoInSaveTowerPos.towerSpriteUpdated;
            //hide update panel
            obj.GetComponent<TowerManager>().upgradePanel.SetActive(false);
            //update currency panel
            CurrencyManager.Instance.UseCurrency(priceforUpgrade);
            //update health
            obj.GetComponent<TowerHealth>().UpgradeTower(saveTowerPos.towerInfoInSaveTowerPos.towerUpdatePower);
            //update attackInterval
            obj.GetComponent<TowerAttackState>().UpgradeTower(saveTowerPos.towerInfoInSaveTowerPos.towerUpdatePower);
            //Can not remove obj form towerList
            canRemove_obj = true;
        }
    }
}
