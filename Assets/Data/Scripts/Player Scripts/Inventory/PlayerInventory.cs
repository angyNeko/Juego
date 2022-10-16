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

        public WeaponItem unarmedWeapon;

        public WeaponItem[] weaponsInRightHandSlot = new WeaponItem[1];
        public WeaponItem[] weaponsInLeftHandSlot = new WeaponItem[1];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;

        public List<WeaponItem> weaponsInventory = new List<WeaponItem>();

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
        }

        private void Start()
        {
            rightHandWeapon = unarmedWeapon;
            leftHandWeapon = unarmedWeapon;
        }

        public void ChangeRightHandWeapon()
        {
            currentRightWeaponIndex++;
            int currentIndex = currentRightWeaponIndex;

            if (currentRightWeaponIndex > weaponsInRightHandSlot.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightHandWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
            }

            if (currentRightWeaponIndex == currentIndex && weaponsInRightHandSlot[currentIndex] != null)
            {
                rightHandWeapon = weaponsInRightHandSlot[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
            }
            else if (currentRightWeaponIndex == currentIndex && weaponsInRightHandSlot[currentIndex] == null)
            {
                currentRightWeaponIndex++;
            }
        }
    }
}