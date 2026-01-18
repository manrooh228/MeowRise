using Assets.Scripts.InventorySystem.Items;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Internal.Commands;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public string catName = "Новый кот";
    [SerializeField] public bool isParent;
    [SerializeField] public bool isMale;
    [SerializeField] public float health;
    [SerializeField] public float damageMultiplier;

    [Header("Items")]
    [SerializeField] public Hat equippedHat;
    [SerializeField] public Weapon equippedWeapon;

    private void Awake()
    {

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
        float bonus = (equippedHat != null) ? equippedHat.healthBonus : 0;
        return health + bonus;
    }

    public float GetTotalDamage()
    {
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
        Debug.Log("Ушел на битву!");
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
