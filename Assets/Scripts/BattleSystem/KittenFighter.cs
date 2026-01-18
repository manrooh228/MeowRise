using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class KittenFighter : BattleUnit
    {
        public RectTransform tr;
        public Cat sourcePocket;

        private Coroutine returnTimerCoroutine;
        private bool isWaitingToReturn = false;

        protected override void Update()
        {
            if (isFighting)
            {
                StopReturnTimer();
                return;
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ant");

            if (enemies.Length > 0)
            {
                StopReturnTimer();
                Move();
            }
            else
            {
                if (!isWaitingToReturn)
                {
                    returnTimerCoroutine = StartCoroutine(ReturnAfterDelay(5f));
                }
            }
        }

        private IEnumerator ReturnAfterDelay(float delay)
        {
            isWaitingToReturn = true;
            yield return new WaitForSeconds(delay);

            ReturnToPocket();
        }

        private void StopReturnTimer()
        {
            if (isWaitingToReturn)
            {
                if (returnTimerCoroutine != null) StopCoroutine(returnTimerCoroutine);
                isWaitingToReturn = false;
            }
        }
        protected override void Move()
        {
            moveSpeed = 100;
            //Debug.Log(tr.localPosition.x);
            if (tr.localPosition.x < 250)
                base.Move();
        }

        private void ReturnToPocket()
        {
            if (sourcePocket != null)
            {
                sourcePocket.ReturnFromBattle(health);
            }
            Destroy(gameObject);
        }

        protected override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            if (health <= 0)
            {
                OnKittenDeath();
            }
        }

        private void OnKittenDeath()
        {
            if (sourcePocket != null)
            {
                sourcePocket.DieFromBattle();
            }
            Destroy(gameObject);
        }
    }
}