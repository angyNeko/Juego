using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        WeaponHolderSlot weaponHolderSlot;

        public WeaponItem leftHandWeapon;
        public WeaponItem rightHandWeapon;

        private void Awake()
        {
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(leftHandWeapon, true);
            weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
        }
    }
}