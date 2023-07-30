using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class Shotgun : ShootWeapon
    {
        protected override void Awake()
        {
            base.Awake();

            this.ammoPrefab = this.m_resLoader.LoadSync<GameObject>("Shotgun Ammo");
        }


        

    }
}