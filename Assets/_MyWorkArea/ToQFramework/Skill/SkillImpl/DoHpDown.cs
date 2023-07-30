using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class DoHpDown : SkillBase
    {
        public override void DoEffect()
        {
            GameModel.AfterGameStart.Register(HpDown);
        }

        public override void ReverseEffect()
        {
            GameModel.AfterGameStart.UnRegister(HpDown);
        }

        /// <summary>
        /// 在所有附加生命值后再计算
        /// </summary>
        private void HpDown()
        {
            int HpMax = PlayerModel.MaxHp;
            while (PlayerModel.Hp > HpMax / 2f)
                PlayerModel.Hp.Value--;
        }
    }
}