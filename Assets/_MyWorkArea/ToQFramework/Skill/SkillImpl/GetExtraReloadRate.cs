using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraReloadRate : SkillBase
    {
        public float reload = 0.2f;

        public override void DoEffect()
        {
            GameModel.SkillReloadRate.Value += reload;
        }

        public override void ReverseEffect()
        {
            GameModel.SkillReloadRate.Value -= reload;
        }
    }
}