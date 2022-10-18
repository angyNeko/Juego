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
        [SerializeField]
        EquipmentWindowUI equipmentWindowUI;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject inventory;
        public GameObject weaponInventoryWindow;
        public GameObject uiInventoryNavbar;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponInventorySlots;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (playerInventory.weaponsInventory.Count == 0)
                    {
                        weaponInventorySlotPrefab.SetActive(false);
                        return;
                    }

                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                    weaponInventoryWindow.SetActive(true);
                }
            }
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

        public void CloseAllInventoryWindows()
        {
            uiInventoryNavbar.SetActive(false);
            weaponInventoryWindow.SetActive(false);
        }

        public void LoadItems()
        {
            equipmentWindowUI.LoadEquipmentsOnEquipmentScreen(playerInventory);
        }


    }
}