using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{

    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public bool isUnarmed;

        [Header("Attack Animation")]
        public string lightAtkAnimation;
        public string heavyAtkAnimation;

        [Header("Item Properties")]
        public int weaponAtk;

        [Header("Weapon Type")]
        public bool sword;
    }
}
