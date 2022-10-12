using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{
    public class WeaponSlotManager : MonoBehaviour
    {
        [SerializeField]
        WeaponHolderSlot leftHandSlot;
        [SerializeField]
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (!isLeft)
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightHandWeaponDamageCollider(weaponItem.weaponAtk);
            }
            else
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftHandWeaponDamageCollider();
            }
        }

        #region Handle Weapon's Damage Collider
        private void LoadLeftHandWeaponDamageCollider()
        {
            try
            {
                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            }
            catch
            {
                leftHandDamageCollider = null;
            }
            //leftHandDamageCollider.SetWeaponDamageValue(weaponAtk);
        }

        private void LoadRightHandWeaponDamageCollider(int weaponAtk)
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.SetWeaponDamageValue(weaponAtk);
        }

        private void OpenLeftHandDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        private void OpenRighttHandDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        private void CloseLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        private void CloseRighttHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }
        #endregion
    }

}