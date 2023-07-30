using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace QFramework.Car
{
    public class DoCrossFire : SkillBase
    {

        public override void DoEffect()
        {
            EnemyModel.OnEnemyDead.Register(CrossFire);
        }

        public override void ReverseEffect()
        {
            EnemyModel.OnEnemyDead.UnRegister(CrossFire);
        }

        private Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        private void CrossFire(Vector3 startPos)
        {
            foreach(var direction in directions)
            {
                var go = ResUtil.GenerateGO("Pistol Ammo", startPos);
                go.transform.rotation = Quaternion.LookRotation(direction);
                go.transform.rotation = Quaternion.Euler(0, go.transform.eulerAngles.y, 0);
                go.transform.localScale = Vector3.one;
                AmmoBase ammo = go.GetComponent<AmmoBase>();
                ammo.projectileMoveWay = new ProjectileMoveStriaght(go.transform, 15, direction);
                ammo.Atk = 10;
                ammo.Pierce = 1;
                ammo.WeaponType = typeof(Pistol);
            }

        }


    }
}