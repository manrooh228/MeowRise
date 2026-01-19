using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class Ant : BattleUnit
    {
        public void Init(int level)
        {
            maxHealth = 50 + (level * 20);
            health = maxHealth;
            damage = 50 + (level * 5);
            enemyTag = "Kitten";
            moveSpeed = 80f;
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.SetHealth(health, maxHealth);
        }

        protected override void Move()
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // Приоритет 1: Котята-бойцы
            if (other.CompareTag("Kitten"))
            {
                targetEnemy = other.GetComponent<BattleUnit>();
                isFighting = true;
                anim.SetTrigger("attack");
                StartCoroutine(AttackCycle());
            }
            // Приоритет 2: Ферма (если котенок не встал на пути)
            else if (other.CompareTag("Farm")) // Не забудьте поставить тег Farm ферме
            {
                CatnipFarm farm = other.GetComponent<CatnipFarm>();
                if (farm != null)
                {
                    isFighting = true;
                    anim.SetTrigger("attack");
                    StartCoroutine(AttackFarmCycle(farm));
                }
            }
        }
        IEnumerator AttackFarmCycle(CatnipFarm farm)
        {
            while (farm != null && farm.health > 0)
            {
                //turn to cat
                anim.SetTrigger("attack");
                farm.TakeDamage(damage);
                yield return new WaitForSeconds(attackSpeed);
            }
            isFighting = false;
        }
    }
}