using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace J
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;

        [SerializeField]
        string lastAttack;
        [SerializeField]
        private int lastAtkNo = 0;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (weapon == null)
                return;

            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                animatorHandler.PlayTargetAnimation(weapon.lightAtkAnimation_1[lastAtkNo], true);
                IncrementResetAtkNo(weapon);
            }
        }

        public void HandleLightAttack(WeaponItem weaponItem)
        {
            if (weaponItem == null || weaponItem.lightAtkAnimation_1.Length < 0)
                return;



            animatorHandler.PlayTargetAnimation(weaponItem.lightAtkAnimation_1[lastAtkNo], true);
            
            IncrementResetAtkNo(weaponItem);
        }

        private void IncrementResetAtkNo(WeaponItem weaponItem)
        {
            lastAttack = weaponItem.lightAtkAnimation_1[lastAtkNo];

            if (lastAtkNo >= (weaponItem.lightAtkAnimation_1.Length - 1))
            {
                lastAtkNo = 0;
            }
            else
            {
                lastAtkNo++;
            }
        }

        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            lastAttack = weaponItem.heavyAtkAnimation[lastAtkNo];

            if (lastAtkNo >= (weaponItem.heavyAtkAnimation.Length - 1))
            {
                lastAtkNo = 0;
            }
            else
            {
                lastAtkNo++;
            }
        }
    }
}
