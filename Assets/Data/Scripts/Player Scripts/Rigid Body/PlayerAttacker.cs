using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        public void HandleLightAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.lightAtkAnimation, true);
        }

        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.heavyAtkAnimation, true);
        }
    }
}
