using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class DoElement : SkillBase
    {
        [EnumPaging]
        public ElementsEnum element;

        public float radius = 5f;


        public Transform TargetTrans;

        public override void DoEffect()
        {
            TargetTrans = PlayerModel.PlayerTrans;
            WeaponModel.OnWeaponReload.Register(SetElement);
        }

        public override void ReverseEffect()
        {
            WeaponModel.OnWeaponReload.UnRegister(SetElement);
        }

        private void SetElement()
        {
            AoeUtil.AoeEffect(TargetTrans.position, radius, null, (buffHandleable, hitPos) =>
            {
                buffHandleable.GetBuffHandler().Add(Type.GetType("QFramework.Car." + element.ToString()), 5);
            });
            
        }
    }
}