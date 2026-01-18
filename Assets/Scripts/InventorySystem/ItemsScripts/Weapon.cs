using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InventorySystem.Items
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
    public class Weapon : Item
    {
        public float damageBonus;
    }
}