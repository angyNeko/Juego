using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class EnemyStats : CharacterStats
    {

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            // rename and uncomment damage animation
            //animator.Play("Damage_01");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                // rename and uncomment damage animation
                //animator.Play("Death_01");

                // remove after fixing previous problems
                Destroy(transform.parent.gameObject);
            }
        }
    }
}