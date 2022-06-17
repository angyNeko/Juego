using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    [CreateAssetMenu(menuName = "Item/Consumables")]
    public class ConsumableItem : Item
    {
        [Header("Item Type")]
        public bool potion;
        public bool food;
        public bool singleUseWeapon;

    }
}