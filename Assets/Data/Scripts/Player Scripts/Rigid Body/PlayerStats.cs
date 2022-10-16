using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private void Awake()
        {
            healthBar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>();
        }

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
            PlayerLocomotion playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Damage_Die", true);

                
                playerLocomotion.rigidbody.velocity = Vector3.zero; // Stops player from moving
            }
            else
            {
                animatorHandler.PlayTargetAnimation("Damage", true);
                playerLocomotion.rigidbody.AddForce(playerLocomotion.rigidbody.transform.position * -300f);
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