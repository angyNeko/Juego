using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace J
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image weaponIcon;
        public Image consumableIcon;
        public Image skill_Slot_1Icon;
        public Image skill_Slot_2Icon;

        public void UpdateWeaponQuickSlotsUI(WeaponItem weapon)
        {
            weaponIcon.sprite = weapon.itemIcon;
            weaponIcon.enabled = true;
        }

        public void UpdateConsumableQuickSlotsUI(ConsumableItem item)
        {
            consumableIcon.sprite = item.itemIcon;
            consumableIcon.enabled = true;
        }

        public void UpdateSkillSlotsUI(bool isLeft,Image image)
        {
            if (isLeft)
            {
                skill_Slot_1Icon = image;
                skill_Slot_1Icon.enabled = true;
            }
            else
            {
                skill_Slot_2Icon = image;
                skill_Slot_2Icon.enabled = true;
            }
        }
    }
}