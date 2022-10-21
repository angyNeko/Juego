using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace J
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        PlayerInventory playerInventory;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject leftPanel;

        [Header("HUD Windows")]
        public GameObject[] quickSlots;
        public GameObject interactAltert;
        public GameObject interactPopUp;
        public GameObject dialogueBoxContainer;
        public GameObject timerContainer;
        public Button pauseButton;

        [Header("Window Panels")]
        public GameObject pausePanel;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots (Reuse Later for inventory)
            //for (int i = 0; i < weaponInventorySlots.Length; i++)
            //{
            //    if (i < playerInventory.weaponsInventory.Count)
            //    {
            //        if (playerInventory.weaponsInventory.Count == 0)
            //        {
            //            weaponInventorySlotPrefab.SetActive(false);
            //            return;
            //        }

            //        if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
            //        {
            //            Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
            //            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            //        }
            //        weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            //    }
            //    else
            //    {
            //        weaponInventorySlots[i].ClearInventorySlot();
            //        weaponInventoryWindow.SetActive(true);
            //    }
            //}
            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllLeftPanelWindow()
        {
            characterCustomizationWindow.SetActive(false);
        }
    }
}