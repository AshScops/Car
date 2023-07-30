using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class RecoverHp : SkillBase
    {
        public int EnemyCnt;

        public override void DoEffect()
        {
            Debug.Log($"每击杀{EnemyCnt}个敌人回复一点生命");
        }

        public override void ReverseEffect()
        {
            Debug.Log("取消注册");
        }
    }
}