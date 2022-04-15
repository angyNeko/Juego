using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeft;
        public bool isRight;

        public GameObject currentWeaponModel;
        
       public void UnloadWeapon() 
       {
            
       }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            if (weaponItem == null)
            {
                //unload wepon
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
    }
}

