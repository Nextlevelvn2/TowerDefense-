using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    [SerializeField] Text lifeText;
    int currentLife;
    int maxLife = 3;
    [SerializeField] GameObject GameOverPanel;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        UpdateLifeUI();
    }
    public void LoseLife()
    {
        currentLife--;
        if (currentLife == 0) LoseGame();
        UpdateLifeUI();
    }
    void LoseGame()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GainLife()
    {
        currentLife++;
        UpdateLifeUI();
    }
    void UpdateLifeUI()
    {
        lifeText.text = currentLife.ToString();
    }
}
