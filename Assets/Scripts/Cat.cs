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

    // Инициализация характеристик
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
        Debug.Log("Ушел на битву!");
    }

    private void OpenCostumization()
    {
        Debug.Log("K1 Button Pressed");
    }

    private void ChangeToParent()
    {
        Debug.Log("Changing to Parent");
    }
}
