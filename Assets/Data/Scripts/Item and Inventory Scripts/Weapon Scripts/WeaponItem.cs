using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{

    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public bool isUnarmed;

        [Header("One Handed Sword Attack Animation")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;

        [Header("Item Properties")]
        public int weaponAtk;

    }
}
