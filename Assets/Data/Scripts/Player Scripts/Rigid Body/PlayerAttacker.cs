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

        public void HandleSwordAttack(WeaponItem weaponItem)
        {
            animatorHandler.PlayTargetAnimation(weaponItem.swordAttack, true);
        }
    }
}
