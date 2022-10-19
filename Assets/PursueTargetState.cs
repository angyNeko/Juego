using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PursueTargetState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //chase the target
            //if within attack range, return combat stance state
            //if target is out of range, return this state and continue to chase target 
            return this;
        }
    }
}
