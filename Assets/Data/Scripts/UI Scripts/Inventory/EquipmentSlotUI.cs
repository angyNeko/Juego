using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace J
{
    public class EquipmentSlotUI : MonoBehaviour
    {
        public Image icon;
        WeaponItem weapon;
        //EquipmentItem equipment;

        public bool headgearSlot;
        public bool armorSlot;
        public bool weaponSlot;
        public bool bootsSlot;

        public void AddWeaponItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            icon.sprite = newWeapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void AddEquipmentItem(/*EquipmentItem newEquipment*/)
        {

        }

        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}