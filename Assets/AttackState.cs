using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPreformingAction)
                return combatStanceState;

            if (currentAttack != null)
            {
                //If too close to enemy, get new attack
                if (enemyManager.distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
                {
                    return this;
                }
                else if (enemyManager.distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
                {
                    //If enemy is within attack viewable, attack
                    if (enemyManager.viewableAngle <= currentAttack.maximumAttackAngle &&
                        enemyManager.viewableAngle >= currentAttack.minimumAttackAngle)
                    {
                        if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPreformingAction == false)
                        {
                            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                            enemyManager.isPreformingAction = true;
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }
            }
            else
            {
                GetNewAttack(enemyManager);
            }
            
            return combatStanceState;  
        }


        #region Attacks

        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetsdirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableangle = Vector3.Angle(targetsdirection, transform.forward);
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxscore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyattackaction = enemyAttacks[i];

                if (enemyManager.distanceFromTarget <= enemyattackaction.maximumDistanceNeededToAttack
                && enemyManager.distanceFromTarget >= enemyattackaction.minimumDistanceNeededToAttack)
                {
                    if (viewableangle <= enemyattackaction.maximumAttackAngle
                    && viewableangle >= enemyattackaction.minimumAttackAngle)
                    {
                        maxscore += enemyattackaction.attackScore;
                    }
                }
            }

            int randomvalue = Random.Range(0, maxscore);
            int temporaryscore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyattackaction = enemyAttacks[i];

                if (enemyManager.distanceFromTarget <= enemyattackaction.maximumDistanceNeededToAttack
                && enemyManager.distanceFromTarget >= enemyattackaction.minimumDistanceNeededToAttack)
                {
                    if (viewableangle <= enemyattackaction.maximumAttackAngle
                    && viewableangle >= enemyattackaction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryscore += enemyattackaction.attackScore;

                        if (temporaryscore > randomvalue)
                        {
                            currentAttack = enemyattackaction;
                        }
                    }
                }
            }
            #endregion
        }
    }
}