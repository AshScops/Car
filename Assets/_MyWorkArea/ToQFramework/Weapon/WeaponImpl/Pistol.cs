using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class Pistol : ShootWeapon
    {
        protected override void Awake()
        {
            this.ammoPrefab = this.m_resLoader.LoadSync<GameObject>("Pistol Ammo");

            base.Awake();
            
        }



    }

}
