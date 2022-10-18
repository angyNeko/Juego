using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool headgearSlotSelected;
        public bool armorSlotSelected;
        public bool weaponSlotSelected;
        public bool bootsSlotSelected;

        EquipmentSlotUI[] slotUI;

        private void Start()
        {
            slotUI = GetComponentsInChildren<EquipmentSlotUI>();
        }

        public void LoadEquipmentsOnEquipmentScreen(PlayerInventory inventory)
        {
            for (int i = 0; i < slotUI.Length; i++)
            {
                if (slotUI[i].headgearSlot)
                {
                    slotUI[i].AddEquipmentItem(); 
                }
                else if (slotUI[i].armorSlot)
                {
                    slotUI[i].AddEquipmentItem();
                }
                else if (slotUI[i].weaponSlot)
                {
                    slotUI[i].AddWeaponItem(inventory.weaponsInRightHandSlot[1]);
                }
                else
                {

                }
            }
        }

        public void SelectHeadgearSlot()
        {
            headgearSlotSelected = true;
        }

        public void SelectArmorSlot()
        {
            armorSlotSelected = true;
        }
        public void SelectWeaponSlot()
        {
            weaponSlotSelected = true;
        }

        public void SelectBootsSlot()
        {
            bootsSlotSelected = true;
        }
    }
}