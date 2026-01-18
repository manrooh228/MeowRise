using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class BattleUnit : MonoBehaviour
    {
        public float health;
        public float damage;
        public float attackSpeed = 1.0f;
        public float moveSpeed = 100f;
        public string enemyTag;

        protected bool isFighting = false;
        protected BattleUnit targetEnemy;

        void Update()
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

        void OnTriggerEnter2D(Collider2D other)
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

        IEnumerator AttackCycle()
        {
            while (targetEnemy != null && targetEnemy.health > 0)
            {
                targetEnemy.TakeDamage(damage);
                yield return new WaitForSeconds(attackSpeed);
            }
            isFighting = false;
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0) Die();
        }

        void Die()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}