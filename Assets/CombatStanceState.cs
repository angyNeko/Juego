using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class CombatStanceState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //check for attack range
            //potentially circle player or walk around them
            //if in attack range return attack state
            //if we are in a cool down after attacking, return this state and continue circling player
            //if the player runs out of range return the pursuetarget state
            return this;
        }
    }
}
