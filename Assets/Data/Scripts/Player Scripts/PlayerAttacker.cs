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
            animatorHandler = GetComponent<AnimatorHandler>();
        }

        public void HandleLightAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.OH_Light_Attack_1, true);
        }

        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.OH_Heavy_Attack_1, true);
        }
    }
}
