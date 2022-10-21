using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyLocomotionManager enemyLocomotionManager;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }

        private void OnAnimatorMove()
        {
<<<<<<< Updated upstream
            float delta = Time.deltaTime;
            enemyLocomotionManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyLocomotionManager.enemyRigidBody.velocity = velocity;
=======
            try
            {
                float delta = Time.deltaTime;
                enemyManager.enemyRigidBody.drag = 0;
                Vector3 deltaPosition = anim.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                enemyManager.enemyRigidBody.velocity = velocity;
            }
            catch
            {
                return;
            }
>>>>>>> Stashed changes
        }
    }
}
