using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class AttackState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //select one of our many attacks based on attack scores
            //if the selected attack is not able to be used because of bad angle or distance, select a new attack
            //if the attack is viable, stop our movement and attack our traget
            //set our recovery timer to the attacks recovery time
            //return the combat stance state

            return this;
        }
    }
}
