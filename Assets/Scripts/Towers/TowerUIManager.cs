using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TowerUIManager : MonoBehaviour
{
    public static TowerUIManager Instance;

    public  List<GameObject> towerList;
    //Button prefab
    public GameObject TowerUIButton;

    //List of Tower Button
    private List<GameObject> btnTowerList;

    //Tower prefab
    public GameObject towerPrefab;

    //SpawnTile
    public Tilemap spawnTile;

    private string towerUIName;

    //Towerprice text
    [SerializeField] private Text towerPriceText;

    //TowerDetails
    [SerializeField] private TowersDetails towersDetails;
    private int towerPrice;
    TowerInfo localTowerInfo;

    Collider2D towerBoxCollider;
    private void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        btnTowerList = new List<GameObject>();
        SpawnTowerUI();
        towerUIName = null;
    }
    // Update is called once per frame
    void Update()
    {
        DetectSpawnTileClick();
    }
    //Spawn Tower Ui
    void SpawnTowerUI()
    {
        for (int i=0; i < towersDetails.towerInfos.Count; i++)
        {
            GameObject obj = Instantiate(TowerUIButton, TowerUIButton.transform.parent);
            //Set name for towerUI
            obj.name = towersDetails.towerInfos[i].towerName;
            //Set price
            obj.gameObject.GetComponentInChildren<Text>().text = towersDetails.towerInfos[i].towerPrice.ToString();
            obj.GetComponent<Image>().sprite = towersDetails.towerInfos[i].towerSprite;
            obj.SetActive(true);
            //Add events to TowerUI Button
            obj.GetComponent<Button>().onClick.AddListener(delegate { SelectTower(obj.name); });
            btnTowerList.Add(obj);
        }
    }

    // UpdateTowerUI function to create new TowerUI when purchased in in the Shop
    public void UpdateTowerUI(TowerInfo towerInfo)
    {
        GameObject obj = Instantiate(TowerUIButton, TowerUIButton.transform.parent);
        //Set name for towerUI
        obj.name = towerInfo.towerName;
        //Set price
        obj.gameObject.GetComponentInChildren<Text>().text = towerInfo.towerPrice.ToString();
        obj.GetComponent<Image>().sprite = towerInfo.towerSprite;
        obj.SetActive(true);
        //Add events to TowerUI Button
        obj.GetComponent<Button>().onClick.AddListener(delegate { SelectTower(obj.name); });
        btnTowerList.Add(obj);
    }
    //Detect selecting TowerUI
    void SelectTower(string TowerUIname)
    {
        if(TowerUIname != towerUIName)
        {
            DeselectTower();
            foreach (GameObject obj in btnTowerList)
            {
                if (obj.name == TowerUIname)
                {
                    obj.GetComponent<Image>().color = new Color(1.4f, 1.4f, 1.4f);
                    towerUIName = TowerUIname;
                }
            }
        }
        else if(TowerUIname == towerUIName)
        {
            foreach (GameObject obj in btnTowerList)
            {
                    obj.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                    towerUIName = null;
            }
        }
    }
    void DeselectTower()
    {
        foreach (GameObject obj in btnTowerList)
        {
            obj.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
    // Detect when mouse is clicked in the tilemap spawning area
    void DetectSpawnTileClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Search for the TowerInFo type to instantiate the right tower as appeard in selected TowerUI
            SetTowerInfo();
            //Detect the cell position through the mousePos
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPos = spawnTile.WorldToCell(mousePos);
            var cellPosCentered = spawnTile.GetCellCenterWorld(cellPos);
            if(spawnTile.GetColliderType(cellPos) == Tile.ColliderType.Sprite && towerUIName != null)
            {
                if (CurrencyManager.Instance.CheckToUseCurrency(towerPrice))
                {
                    SpawnTowerInTileMap(cellPosCentered, cellPos);
                }
            }
        }
    }
    void SpawnTowerInTileMap(Vector3 cellPosCentered, Vector3Int cellPos)
    {
        GameObject obj = Instantiate(towerPrefab, towerPrefab.transform.parent);
        //Set Info for tower
        obj.name = towerUIName;
        obj.SetActive(true);
        obj.transform.position = cellPosCentered;

        //Set the colliderType to none that means cell is occupied
        spawnTile.SetColliderType(cellPos, Tile.ColliderType.None);
        CurrencyManager.Instance.UseCurrency(towerPrice);
        towerUIName = null;
        obj.GetComponent<TowerManager>().towerPosition = cellPosCentered;

        //save the name and the position to upgrade button, to know which tower will be upgraded when the button is clicked
        SaveTowerPos saveInfo = obj.transform.GetChild(0).GetChild(0).GetComponent<SaveTowerPos>();
        saveInfo.towerPos = cellPosCentered;
        saveInfo.towerName = towerUIName;
        saveInfo.towerInfoInSaveTowerPos = localTowerInfo;
        towerList.Add(obj);

        //Enable box collider
        towerBoxCollider = obj.GetComponent<BoxCollider2D>();
        Invoke("EnableBoxeCollider", 2f);
    }
    void SetTowerInfo()
    {
        foreach (TowerInfo tower in towersDetails.towerInfos)
        {
            if (towerUIName == tower.towerName)
            {
                towerPrice = tower.towerPrice;
                localTowerInfo = tower;
                break;
            }
        }
    }
    void EnableBoxeCollider()
    {
        towerBoxCollider.enabled = true;
    }
}
[System.Serializable]
public class TowerUI
{
    public string name;
    public int price;
}
