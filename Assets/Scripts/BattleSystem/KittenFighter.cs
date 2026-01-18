using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BattleSystem
{
    public class KittenFighter : BattleUnit
    {
        public RectTransform tr;
        protected override void Move()
        {
            moveSpeed = 100;
            Debug.Log(tr.localPosition.x);
            if(tr.localPosition.x < 250)
                base.Move();
        }
    }
}