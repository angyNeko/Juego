using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;
        WeaponHolderSlot weaponHolderSlot;

        public WeaponItem rightHandWeapon;
        public WeaponItem leftHandWeapon;

        private void Awake()
        {
            weaponSlotManager = GetComponent<WeaponSlotManager>();

        }

        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
            weaponSlotManager.LoadWeaponOnSlot(leftHandWeapon, true);
        }
    }
}