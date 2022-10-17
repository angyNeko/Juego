using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace J
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        public Image icon;
        WeaponItem weapon;

        public void AddItem(WeaponItem newWeapon)
        {
            if (newWeapon == null)
                return;

            weapon = newWeapon;
            icon.sprite = newWeapon.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            weapon = null;
            icon = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
