using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG 
{

    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Sword Attack Animation")]
        public string OH_Light_Attack_1;
        public string OH_Heavy_Attack_1;

    }
}
