using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectionPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public List<LevelDetails> levelDetails;
    int currentLevel = 1;
    int maxLevel = 2;
    [SerializeField] Button backBtn, nextBtn, playBtn;
    [SerializeField] Image levelImg , currentLevelPos;
    void Start()
    {
        nextBtn.GetComponent<Button>().onClick.AddListener(NextBtnClick);
        backBtn.GetComponent<Button>().onClick.AddListener(BackBtnClick);
        playBtn.GetComponent<Button>().onClick.AddListener(LoadGame);
    }
    void LoadGame()
    {
        SceneManager.LoadScene(currentLevel);
        Time.timeScale = 1;
    }
    void NextBtnClick()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
        }
    }
    void BackBtnClick()
    {
        if (currentLevel >1)
        {
            currentLevel--;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMapSelectionPanel();
    }
    void UpdateMapSelectionPanel()
    {
        currentLevelPos.transform.position = levelDetails[currentLevel - 1].levelPosOnMenu.transform.position;
        levelImg.GetComponent<Image>().sprite = levelDetails[currentLevel - 1].levelSprite;
    }
}
[System.Serializable]
public class LevelDetails
{
    public int currentLevel;
    public Sprite levelSprite;
    public GameObject levelPosOnMenu;
}
