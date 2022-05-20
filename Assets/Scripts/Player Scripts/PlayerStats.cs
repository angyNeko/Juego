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

        [SerializeField] HealthBar healthBar;
        [SerializeField] PlayerController playerController;
        [SerializeField] AnimatorManager animatorManager;

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private void Update()
        {
            if (healthBar.slider.value == 0)
            {
                animatorManager.PlayTargetAnimation("Dead", true);
                GameObject.Find("Respawn Manager").GetComponent<RespawnManager>().Invoke("TriggerRespawn", 5f);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                playerController.HandleDeath();
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
            animatorManager.PlayTargetAnimation("Revive", true);
        }
    }
}