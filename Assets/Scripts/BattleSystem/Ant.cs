using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class Ant : BattleUnit
    {
        public void Init(int level)
        {
            health = 50 + (level * 20);
            damage = 50 + (level * 5);
            enemyTag = "Kitten";
            moveSpeed = 80f;
        }

        protected override void Move()
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}