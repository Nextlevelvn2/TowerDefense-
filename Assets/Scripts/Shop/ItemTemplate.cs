using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemTemplate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI towerTitle, towerDescription, towerPrice;
    [SerializeField] Button purchaseBtn;
    [SerializeField] TowersDetails towersDetails;
    public TowerShopTemPlate towerShopTemPlate;
    [SerializeField] Image towerImg;
    [SerializeField] GameObject purchaseImg;
    [SerializeField] TextMeshProUGUI purchaseIndicator;
    public float timeTurnOffPurchaseIndicator;
    public GameObject scrollView;
    // Start is called before the first frame update
    void Start()
    {
        ChangeItemTemplate();
    }

    void ChangeItemTemplate()
    {
        towerTitle.text = towerShopTemPlate.towerInfo.towerName;
        towerDescription.text = towerShopTemPlate.itemDescription;
        towerPrice.text = "Stars: " + towerShopTemPlate.towerPrice.ToString();
        towerImg.GetComponent<Image>().sprite = towerShopTemPlate.towerInfo.towerSprite;
        purchaseBtn.onClick.AddListener(UpdateTowerUI);
    }
    void UpdateTowerUI()
    {
        if (CurrencyManager.Instance.CheckToUseStar(towerShopTemPlate.towerPrice))
        {
            purchaseIndicator.text = "Successfully Purchased";
            TowerUIManager.Instance.UpdateTowerUI(towerShopTemPlate.towerInfo);
            towersDetails.towerInfos.Add(towerShopTemPlate.towerInfo);
            DataManager.Instance.UpdateTowerDicTionary(towerShopTemPlate.towerInfo);
            CurrencyManager.Instance.UseStar(towerShopTemPlate.towerPrice);
            purchaseBtn.gameObject.SetActive(false);
        }
        else
        {
            purchaseIndicator.text = "Not enough stars";
        }
        purchaseImg.SetActive(true);
        scrollView.SetActive(false);
    }
}
