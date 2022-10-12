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

        [SerializeField]
        bool leftWeaponLoaded;
        [SerializeField]
        bool rightWeaponLoaded;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            weaponHolderSlot = GetComponentInChildren<WeaponHolderSlot>();
        }

        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(leftHandWeapon, true);
            weaponSlotManager.LoadWeaponOnSlot(rightHandWeapon, false);
        }

        private void Update()
        {
            //leftWeaponLoaded = weaponHolderSlot.currentWeaponModel;
            rightWeaponLoaded = weaponHolderSlot.currentWeaponModel;
        }
    }
}