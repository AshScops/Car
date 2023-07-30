
using System;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public abstract class SkillBase
    {
        protected GameModel GameModel => GameArch.Interface.GetModel<GameModel>();
        protected PlayerModel PlayerModel => GameArch.Interface.GetModel<PlayerModel>();
        protected EnemyModel EnemyModel => GameArch.Interface.GetModel<EnemyModel>();
        protected WeaponModel WeaponModel => GameArch.Interface.GetModel<WeaponModel>();

        /// <summary>
        /// 原子操作
        /// </summary>
        public abstract void DoEffect();

        /// <summary>
        /// 原子操作
        /// </summary>
        public abstract void ReverseEffect();
    }
}