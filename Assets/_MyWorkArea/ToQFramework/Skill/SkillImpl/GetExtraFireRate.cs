using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraFireRate : SkillBase
    {
        public float fire = 0.2f;

        public override void DoEffect()
        {
            GameModel.SkillFireRate.Value += fire;
        }

        public override void ReverseEffect()
        {
            GameModel.SkillFireRate.Value -= fire;
        }
    }
}