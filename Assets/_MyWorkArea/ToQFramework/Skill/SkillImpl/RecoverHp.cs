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
            Debug.Log($"ÿ��ɱ{EnemyCnt}�����˻ظ�һ������");
        }

        public override void ReverseEffect()
        {
            Debug.Log("ȡ��ע��");
        }
    }
}