using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace J
{
    public class CharacterCustomizationWindowUI : MonoBehaviour
    {
        public bool weaponSlotSelected;

        public void SelectWeaponSlot()
        {
            weaponSlotSelected = true;
        }
    }
}