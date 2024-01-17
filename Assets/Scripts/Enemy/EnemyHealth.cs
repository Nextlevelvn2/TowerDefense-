using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    int maxHealth;
    public int gainAmount;
    List<Collider2D> collisionsList = new List<Collider2D>();
    [SerializeField] Slider HealthSlider;
    //Audio
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        maxHealth = Health;
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            GameEvents.OnEnemyDie(this);
        }
        if (Health < 0) Health = 0;
        UpdateHealthBar(Health, maxHealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bulletTower")
        {
            collisionsList.Add(collision);
        }
    }
    public bool IsDead()
    {
        if (Health <= 0)
        {
            PlayDieSound();
            return true;
        }
        else return false;
    }
    private void Update()
    {
        if (Health <= 0)
        {

            Health = 0;
        }
    }
    public void PlayDieSound()
    {
        audioSource.enabled = true;
        audioSource.Play();
    }
    void UpdateHealthBar(int currentValue, int maxValue)
    {
        HealthSlider.value = currentValue / maxValue;
    }
}
