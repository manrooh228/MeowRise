using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class KittenFighter : BattleUnit
    {
        protected override void Move()
        {
            moveSpeed = 100f;
            base.Move();
        }
    }
}