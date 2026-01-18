using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "New Hat", menuName = "Inventory/Hat")]
    public class Hat : Item
    {
        public float healthBonus;
    }
}