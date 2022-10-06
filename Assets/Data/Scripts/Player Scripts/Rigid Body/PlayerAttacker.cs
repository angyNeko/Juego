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

        public void HandleAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.attackAnim, true);
        }
    }
}
