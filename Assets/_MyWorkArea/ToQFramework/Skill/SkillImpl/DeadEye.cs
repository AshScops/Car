using System;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class DeadEye : SkillBase
    {
        public int PerCnt = 3;
        private int cnt = 0;

        public override void DoEffect()
        {
            WeaponModel.OnAmmoGenerate.Register(GiveBulletStrength);
        }

        public override void ReverseEffect()
        {
            WeaponModel.OnAmmoGenerate.Register(GiveBulletStrength);
        }

        private void GiveBulletStrength(GameObject ammoGo, Vector3 startPos, Vector3 targetPos)
        {
            if(cnt < PerCnt)
            {
                cnt++;
                return;
            }

            cnt = 0;
            AmmoBase ammo;
            if (!ammoGo.TryGetComponent(out ammo)) return;
            ammo.Atk *= 2f;
            ammo.transform.localScale *= 2f;
        }
    }
}