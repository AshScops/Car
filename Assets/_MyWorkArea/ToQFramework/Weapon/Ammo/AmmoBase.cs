using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{

    [RequireComponent(typeof(SphereCollider))]
    public class AmmoBase : ViewController
    {
        public Type WeaponType;

        public ProjectileMoveBase projectileMoveWay;


        [SerializeField]
        private float atk = 0;
        public float Atk { get => atk; set => atk = value; }

        public int Pierce = 1;

        public float LifeCd = 3f;

        protected CooldownTimer lifeCdTimer;

        protected ResLoader resLoader = ResLoader.Allocate();


        protected WeaponModel weaponModel;
        public List<Type> BuffTypesOnHit = new List<Type>();

        [SerializeField]
        protected string hitFxName = "Ammo Hit";

        protected virtual void Start()
        {
            lifeCdTimer = new CooldownTimer(this.LifeCd);
            weaponModel = GameArch.Interface.GetModel<WeaponModel>();
        }

        protected void Update()
        {
            if (GameArch.Interface.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if (lifeCdTimer.CoolDownOnUpdate(Time.deltaTime))
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (GameArch.Interface.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate()
        {
            projectileMoveWay?.Move();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HandleHitEnemy(other);

                Pierce--;
                if (Pierce > 0) return;

                Destroy(this.gameObject);
            }
        }

        protected virtual void HandleHitEnemy(Collider other)
        {
            IDamageable damageable;
            if (other.transform.TryGetComponent(out damageable))
            {
                damageable.GetDamage(this.atk);
                weaponModel.OnAmmoHit.Trigger(gameObject, other.ClosestPoint(transform.position));

                BuffHandleable buffHandleable = null;
                if (other.gameObject.TryGetComponent<BuffHandleable>(out buffHandleable))
                {
                    for (int i = 0; i < BuffTypesOnHit.Count; i++)
                    {
                        buffHandleable.GetBuffHandler().Add(BuffTypesOnHit[i]);
                    }
                }
                BuffTypesOnHit.Clear();
            }

            ResUtil.GenerateGO(hitFxName, other.ClosestPoint(this.transform.position));
        }


    }

}
