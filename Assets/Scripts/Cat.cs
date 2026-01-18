using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework.Internal.Commands;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public bool isParent;
    public bool isMale;
    public float health;
    public float damageMultiplier;

    private void Awake()
    {

    }

    // »нициализаци€ характеристик
    public void Init(float hp, float dmg, bool isParent, bool isMale)
    {
        this.health = hp;
        this.damageMultiplier = dmg;
        this.isParent = isParent;
    }

    public void K1buttonOnClick()
    {
        OpenCostumization();
    }
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
        Debug.Log("”шел на битву!");
    }

    private void OpenCostumization()
    {
        if (CustomizationMenu.Instance != null)
        {
            CustomizationMenu.Instance.Open(this);
        }
        else
        {
            Debug.LogError("CustomizationMenu не найден на сцене!");
        }
    }

    private void ChangeToParent()
    {
        Debug.Log("Changing to Parent");
    }
}
