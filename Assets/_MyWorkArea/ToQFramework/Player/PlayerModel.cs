using UnityEngine;

namespace QFramework.Car
{
    public enum PlayerHpState
    {
        canGetDamage = 0,
        loadDamageCd,
        dead
    }

    public class PlayerModel : AbstractModel
    {
        public Transform PlayerTrans;

        public float MoveSpeed = 3f;
        public float RotSpeed = 10f;

        //TODO:更新一堆UI
        public const float RAW_HURT_CD = 2f;
        private CooldownTimer m_hurtTimer;
        public EasyEvent PlayerHurtEvent;

        public BindableProperty<int> MaxHp;
        public BindableProperty<int> Hp;
        public BindableProperty<PlayerHpState> CurrentState;

        public float CollectRadius = 3f;

        public BindableProperty<int> CurrentExp;
        public BindableProperty<int> LevelUpExpUpperLimit;
        public BindableProperty<int> CurrentLevel;

        public PlayerPower PlayerPower;
        public PlayerWeapon PlayerWeapon;
        public PlayerSkill PlayerSkill;

        protected override void OnInit()
        {
            MoveSpeed = 3f;
            RotSpeed = 10f;

            m_hurtTimer = new CooldownTimer(RAW_HURT_CD);
            PlayerHurtEvent = new EasyEvent();
            MaxHp = new BindableProperty<int>(3);
            Hp = new BindableProperty<int>(MaxHp);
            CurrentState = new BindableProperty<PlayerHpState>(PlayerHpState.canGetDamage);

            CollectRadius = 3f;

            CurrentExp = new BindableProperty<int>(0);
            LevelUpExpUpperLimit = new BindableProperty<int>(5);
            CurrentLevel = new BindableProperty<int>(1);

            PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            
            PlayerPower = new PlayerPower();
            PlayerPower.Init();

            PlayerWeapon = new PlayerWeapon();
            PlayerWeapon.Init(PlayerTrans.GetComponent<Player>().WeaponRoot);

            PlayerSkill = new PlayerSkill();
            PlayerSkill.Init();

            PlayerHurtEvent.Register(() =>
            {
                if (m_hurtTimer.GetCoolDownResult())
                {
                    Hp.Value--;
                    m_hurtTimer.ResetCD(RAW_HURT_CD);

                    //玩家模型闪白
                    var player = PlayerTrans.GetComponent<Player>();
                    player.StartCoroutine(player.HitFlash(RAW_HURT_CD));
                }
            });

            MaxHp.Register((maxHp) =>
            {
                Hp.Value = maxHp;
            });

            Hp.Register((hp) =>
            {
                if (hp <= 0)
                    GameArch.Interface.GetSystem<GameSystem>().GameOver();
            });

            GameArch.Interface.GetModel<GameModel>().ResetAllValue.Register(() =>
            {
                MoveSpeed = 3f;
                RotSpeed = 10f;

                m_hurtTimer = new CooldownTimer(RAW_HURT_CD);
                MaxHp.Value = 3;
                CurrentState.Value = PlayerHpState.canGetDamage;

                CollectRadius = 3f;
                CurrentExp.Value = 0;
                LevelUpExpUpperLimit.Value = 5;
                CurrentLevel.Value = 1;

                PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
                Debug.Log("PlayerTrans: " + PlayerTrans);
                PlayerPower = new PlayerPower();
                PlayerPower.Init();

                PlayerWeapon = new PlayerWeapon();
                PlayerWeapon.Init(PlayerTrans.GetComponent<Player>().WeaponRoot);
            });
        }

        public void OnUpdate()
        {
            PlayerPower.OnUpdate();
            m_hurtTimer.CoolDownOnUpdate(Time.deltaTime);
        }







    }
}