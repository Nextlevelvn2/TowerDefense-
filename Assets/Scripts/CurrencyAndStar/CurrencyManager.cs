using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    //Field
    //StartCurrency
    public int StartCurrency =18;
    //currentCurrency
    public int CurrentCurrency;
    //TextCurrency
    [SerializeField] private Text textCurrency;
    //start star
    //StartCurrency
    int StartStar = 40;
    //currentStar
    public int CurrentStar;
    //TextCurrency
    [SerializeField] private Text textStar;
    //star instantiate
    [SerializeField] private GameObject startPopUp;
    private int popUpRate = 20;
    public Queue<GameObject> stars;
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
    private void Start()
    {
        Init();
    }
    //method
    public void Init()
    {
        CurrentCurrency = StartCurrency;
        UpdateUICurrency();
        CurrentStar = StartStar;
        UpdateUIStar();
        stars = new Queue<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            GameObject star = Instantiate(startPopUp, new Vector3(-3f,0.5f,0f), Quaternion.identity);
            star.SetActive(false);
            stars.Enqueue(star);
        }
    }
    //get star
   //gainCurrency
   public void GainCurrency(EnemyHealth enemyHealth)
    {
        CurrentCurrency += enemyHealth.gainAmount;
        UpdateUICurrency();
        if (Random.Range(0, 100) <= popUpRate && Time.time>2f)
        {
            GameObject star = stars.Dequeue();
            star.transform.position = enemyHealth.gameObject.transform.position;
            //GameObject star = Instantiate(startPopUp, enemyHealth.gameObject.transform.position, Quaternion.identity);
            star.SetActive(true);
        }
    }
   //UseCurrency
   public void UseCurrency(int amount)
    {
        if (CheckToUseCurrency(amount))
        {
            CurrentCurrency -= amount;
            UpdateUICurrency();
        }
    }
    //CheckAvailableCUrrency
    public bool CheckToUseCurrency(int amount)
    {
        if (amount <= CurrentCurrency) return true;
        else return false;
    }
   //UpdateUICurrency
   void UpdateUICurrency()
    {
        textCurrency.GetComponent<Text>().text = CurrentCurrency.ToString();
    }
    //Subcribe to EnemyDie Event
    private void OnEnable()
    {
        GameEvents.OnEnemyDie += GainCurrency;
    }
    private void OnDisable()
    {
        GameEvents.OnEnemyDie -= GainCurrency;
    }
    #region Star
    public void GainStar(int amount)
    {
        CurrentStar += amount;
        UpdateUIStar();
    }
    //UseStar
    public void UseStar(int amount)
    {
        if (CheckToUseStar(amount))
        {
            CurrentStar -= amount;
            UpdateUIStar();
        }
    }
    //CheckAvailableStar
    public bool CheckToUseStar(int amount)
    {
        if (amount <= CurrentStar) return true;
        else return false;
    }
    //UpdateUIStar
    void UpdateUIStar()
    {
        textStar.GetComponent<Text>().text = CurrentStar.ToString();
    }
    #endregion
}
