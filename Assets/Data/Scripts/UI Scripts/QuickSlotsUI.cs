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
            if (weapon.itemIcon != null)
            {
                weaponIcon.sprite = weapon.itemIcon;
                weaponIcon.enabled = true;
            }
            else
            {
                weaponIcon.sprite = null;
                weaponIcon.enabled = false;
            }
        }

        public void UpdateConsumableQuickSlotsUI(ConsumableItem item)
        {
            if(item.itemIcon != null)
            {
                consumableIcon.sprite = item.itemIcon;
                consumableIcon.enabled = true;
            }
            else
            {
                consumableIcon.sprite = null;
                consumableIcon.enabled = false;
            }
        }

        public void UpdateSkillSlotsUI(bool isLeft,WeaponItem weapon)
        {
            if (isLeft)
            {
                if (weapon.skillQIcon != null)
                {
                    skill_Slot_1Icon.sprite = weapon.skillQIcon;
                    skill_Slot_1Icon.enabled = true;
                }
                else
                {
                    skill_Slot_1Icon.sprite = null;
                    skill_Slot_1Icon.enabled = false;
                }
            }
            else
            {
                if (weapon.skillEIcon != null)
                {
                    skill_Slot_2Icon.sprite = weapon.skillEIcon;
                    skill_Slot_2Icon.enabled = true;
                }
                else
                {
                    skill_Slot_2Icon.sprite = null;
                    skill_Slot_2Icon.enabled = false;
                }
            }
        }
    }
}