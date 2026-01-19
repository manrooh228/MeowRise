using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class BattleUnit : MonoBehaviour
    {
        public float maxHealth;
        public float health;
        public float damage;
        public float attackSpeed = 1.0f;
        public float moveSpeed = 100f;
        public string enemyTag;

        
        public HealthBar healthBar;
        protected bool isFighting = false;
        protected BattleUnit targetEnemy;


        protected virtual void Update()
        {
            if (!isFighting)
            {
                Move();
            }
        }

        protected virtual void Move()
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * (enemyTag == "Ant" ? 1 : -1));
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(enemyTag))
            {
                targetEnemy = other.GetComponent<BattleUnit>();
                if (targetEnemy != null)
                {
                    isFighting = true;
                    StartCoroutine(AttackCycle());
                }
            }
        }

        protected IEnumerator AttackCycle()
        {
            while (targetEnemy != null && targetEnemy.health > 0)
            {
                targetEnemy.TakeDamage(damage);
                yield return new WaitForSeconds(attackSpeed);
            }
            isFighting = false;
        }

        protected virtual void TakeDamage(float amount)
        {
            health -= amount;

            if (healthBar != null)
            {
                healthBar.SetHealth(health, maxHealth);
            }

            if (health <= 0) Die();
        }

        void Die()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}