using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraHp : SkillBase
    {

        public int extraHp;

        public override void DoEffect()
        {
            PlayerModel.MaxHp.Value += extraHp;
        }

        public override void ReverseEffect()
        {
            PlayerModel.MaxHp.Value -= extraHp;
        }
    }
}