using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        [SerializeField] 
        HealthBar healthBar;
        [SerializeField] 
        PlayerManager playerManager;
        [SerializeField] 
        AnimatorHandler animatorHandler;

        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();

            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private void Update()
        {
            if (healthBar.slider.value == 0)
            {
                animatorHandler.PlayTargetAnimation("Dead", true);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            // Modify later
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Damage_Die", true);
            }
            else
            {
                animatorHandler.PlayTargetAnimation("Damage", true);
            }
        }

        public void Heal(int heal)
        {
            if (heal <= currentHealth)
            {
                currentHealth = currentHealth + heal;
            }

            else
            {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }

        public void Revive()
        {
            healthBar.SetCurrentHealth(maxHealth);
            animatorHandler.PlayTargetAnimation("Revive", true);
        }
    }
}