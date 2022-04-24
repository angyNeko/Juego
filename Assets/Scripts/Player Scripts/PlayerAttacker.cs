using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorManager animatorManager;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
        }
        public void HandleLightAttack(WeaponItem weaponItem)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Light_Attack_1, true);
        }

        public void HandleHeavyAttack(WeaponItem weaponItem)
        {
            animatorManager.PlayTargetAnimation(weaponItem.OH_Heavy_Attack_1, true);
        }
    }
}
