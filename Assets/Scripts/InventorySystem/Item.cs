using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public abstract class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public string description;
    }
}