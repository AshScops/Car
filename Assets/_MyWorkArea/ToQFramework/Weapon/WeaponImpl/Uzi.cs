using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class Uzi : ShootWeapon
    {

        protected override void Awake()
        {

            this.ammoPrefab = m_resLoader.LoadSync<GameObject>("Uzi Ammo");

            base.Awake();


        }

    }
}