using TMPro;
using Assets.Scripts.BattleSystem;
using Assets.Scripts.InventorySystem.Items;
using NUnit.Framework.Internal.Commands;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System.Runtime.Serialization;

public class Cat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public string catName = "Новый кот";
    [SerializeField] public bool isParent;
    [SerializeField] public bool isMale;
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float damageMultiplier;
    [SerializeField] public Transform battleMap;
    //[SerializeField] public Transform spawnPoint;
    [SerializeField] public GameObject kittenFighter;


    [Header("Identicators")]
    [SerializeField] public GameObject inBattleIdenticator;
    [SerializeField] public GameObject k1Button;
    [SerializeField] public GameObject k2Button;
    [SerializeField] public TMP_Text nameText;
    [SerializeField] public HealthBar healthBar;
    [SerializeField] public BreedingHandler bh;

    [Header("Items")]
    [SerializeField] public Hat equippedHat;
    [SerializeField] public Weapon equippedWeapon;

    

    private void Awake()
    {
        battleMap = GameObject.Find("SpawnPointKittens").GetComponent<Transform>();
        if(!isParent)
            inBattleIdenticator.SetActive(false);
        
    }

    // Инициализация характеристик
    public void Init(float hp, float dmg, bool isParent, bool isMale, HealthBar healthBar)
    {
        this.maxHealth = hp;
        this.health = hp;
        this.damageMultiplier = dmg;
        this.isParent = isParent;
        this.healthBar = healthBar;

        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }
    }
    public void ChangeName(string newName)
    {
        if (nameText != null)
        {
            catName = newName;
            nameText.text = newName;
        }
    }
    
    //Бонусы от оружии, короче
    public float GetTotalHealth()
    {
        if(equippedHat == null)
            return maxHealth;
        
        float bonus = (equippedHat != null) ? equippedHat.healthBonus : 0;
        return maxHealth + bonus;
    }

    public float GetTotalDamage()
    {
        if (equippedWeapon == null)
            return health;

        float bonus = (equippedWeapon != null) ? equippedWeapon.damageBonus : 0;
        return damageMultiplier + bonus;
    }

    public void K1buttonOnClick() => OpenCostumization(); 

    public void K2buttonOnClick()
    {
        if (!isParent)
        {
            SendToBattle();
        }
        else
        {
            ChangeToParent();
        }
    }

    public void OnKittenClicked()
    {
        // Ищем BreedingHandler на сцене
        BreedingHandler handler = FindFirstObjectByType<BreedingHandler>();
        if (handler != null)
        {
            handler.SelectNewParent(this);
        }
    }

    public void ReturnFromBattle(float remainingHealth)
    {
        this.health = remainingHealth;

        if (healthBar != null)
        {
            healthBar.SetHealth(this.health, this.maxHealth);
        }

        k1Button.SetActive(true);
        k2Button.SetActive(true);
        inBattleIdenticator.SetActive(false);
    }

    public void DieFromBattle()
    {
        Debug.Log("Котенок погиб в бою. Удаляем карман.");
        // Уничтожаем объект кармана в интерфейсе
        Destroy(gameObject);
    }

    private void SendToBattle()
    {
        GameObject spawnPoint = GameObject.Find("SpawnPointKittens");
        

        if (spawnPoint != null)
        {
            GameObject fighter = Instantiate(kittenFighter, spawnPoint.transform.position, Quaternion.identity, battleMap);
            this.healthBar.healthText.text = "";
            this.nameText.text = "";

            KittenFighter unit = fighter.GetComponent<KittenFighter>();
            unit.sourcePocket = this;
            unit.maxHealth = GetTotalHealth();
            unit.health = GetTotalHealth();
            unit.damage = GetTotalDamage();
            unit.enemyTag = "Ant";
            unit.healthBar = unit.GetComponentInChildren<HealthBar>();
            unit.healthBar.SetHealth(health, maxHealth);

            k1Button.SetActive(false);
            k2Button.SetActive(false);
            inBattleIdenticator.SetActive(true);

            Destroy(fighter.GetComponent<CanvasGroup>());
        }

    }


    private void OpenCostumization()
    {
        Time.timeScale = 0f;
        if (CustomizationMenu.Instance != null)
            CustomizationMenu.Instance.Open(this);
    }

    private void ChangeToParent()
    {
        if (bh != null) // Проверка предотвращает ошибку на строке 174
        {
            bh.StartReplaceParent(isMale);
        }
        else
        {
            // Автоматический поиск, если ссылка не задана в инспекторе
            bh = FindFirstObjectByType<BreedingHandler>();
            if (bh != null) bh.StartReplaceParent(isMale);
        }
    }


}
