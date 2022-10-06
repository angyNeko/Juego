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
        public string attackAnim;

        [Header("Item Properties")]
        public int weaponAtk;

        [Header("Weapon Type")]
        public bool sword;
    }
}
