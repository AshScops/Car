using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class GetExtraDamageRate : SkillBase
    {
        public float damageRate = 0.5f;
        public override void DoEffect()
        {
            GameModel.SkillAtkRate.Value += damageRate;
        }

        public override void ReverseEffect()
        {
            GameModel.SkillAtkRate.Value -= damageRate;
        }

    }
}