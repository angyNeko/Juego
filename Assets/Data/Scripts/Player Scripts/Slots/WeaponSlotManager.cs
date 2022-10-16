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

        Animator animator;

        QuickSlotsUI quickSlots;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlots = FindObjectOfType<QuickSlotsUI>();

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
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftHandWeaponDamageCollider();
                #region Handle Left Weapon Idle Animations
                if (weaponItem != null)
                {
                    //animator.CrossFade(weaponItem.leftHandIdle, 0.2f);
                }
                else
                {
                    //animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightHandWeaponDamageCollider(weaponItem.weaponAtk);
                quickSlots.UpdateWeaponQuickSlotsUI(weaponItem);
                #region Handle Right Weapon Idle Animations
                if (weaponItem != null && weaponItem.name != "Unarmed")
                {
                    animator.CrossFade(weaponItem.rightHandIdle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
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
            try
            {
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            }
            catch
            {
                rightHandDamageCollider.SetWeaponDamageValue(weaponAtk);
            }
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