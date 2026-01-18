using Assets.Scripts.BattleSystem;
using Assets.Scripts.InventorySystem.Items;
using NUnit.Framework.Internal.Commands;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public string catName = "Новый кот";
    [SerializeField] public bool isParent;
    [SerializeField] public bool isMale;
    [SerializeField] public float health;
    [SerializeField] public float damageMultiplier;
    [SerializeField] public Transform battleMap;
    //[SerializeField] public Transform spawnPoint;
    [SerializeField] public GameObject kittenFighter;

    [Header("Identicators")]
    [SerializeField] public GameObject inBattleIdenticator;
    [SerializeField] public GameObject k1Button;
    [SerializeField] public GameObject k2Button;

    [Header("Items")]
    [SerializeField] public Hat equippedHat;
    [SerializeField] public Weapon equippedWeapon;

    

    private void Awake()
    {
        battleMap = GameObject.Find("SpawnPointKittens").GetComponent<Transform>();
        inBattleIdenticator.SetActive(false);
    }

    // Инициализация характеристик
    public void Init(float hp, float dmg, bool isParent, bool isMale)
    {
        this.health = hp;
        this.damageMultiplier = dmg;
        this.isParent = isParent;
    }
    public void ChangeName(string newName)
    {
        catName = newName;
        gameObject.name = newName;
    }
    
    //Бонусы от оружии, короче
    public float GetTotalHealth()
    {
        if(equippedHat == null)
            return health;
        
        float bonus = (equippedHat != null) ? equippedHat.healthBonus : 0;
        return health + bonus;
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

    private void SendToBattle()
    {
        GameObject spawnPoint = GameObject.Find("SpawnPointKittens");
        

        if (spawnPoint != null)
        {
            GameObject fighter = Instantiate(kittenFighter, spawnPoint.transform.position, Quaternion.identity, battleMap);

            KittenFighter unit = fighter.GetComponent<KittenFighter>();
            unit.health = GetTotalHealth();
            unit.damage = GetTotalDamage();
            unit.enemyTag = "Ant";

            k1Button.SetActive(false);
            k2Button.SetActive(false);
            inBattleIdenticator.SetActive(true);

            Destroy(fighter.GetComponent<CanvasGroup>());
        }

    }


    private void OpenCostumization()
    {
        if (CustomizationMenu.Instance != null)
            CustomizationMenu.Instance.Open(this);
    }

    private void ChangeToParent()
    {
        Debug.Log("Changing to Parent");
    }
}
