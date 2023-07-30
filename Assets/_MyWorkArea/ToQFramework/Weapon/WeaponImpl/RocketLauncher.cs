using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    public class RocketLauncher : ShootWeapon
    {


        protected override void Awake()
        {
            this.ammoPrefab = this.m_resLoader.LoadSync<GameObject>("Rocket Ammo");

            base.Awake();

        }


    }
}