using UnityEngine;
using System.Collections;

namespace QFramework.Car
{
	public partial class GameController : ViewController, IController, ISingleton
	{
        private ResLoader m_resLoader = ResLoader.Allocate();
        private GameModel m_gameModel;
        private PlayerModel m_playerModel;
        private EnemyModel m_enemyModel;
        private GameSystem m_gameSystem;

        public void OnSingletonInit()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public static GameController Instance
        {
            get { return MonoSingletonProperty<GameController>.Instance; }
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        private void Awake()
        {   
            m_gameModel = this.GetModel<GameModel>();
            m_playerModel = this.GetModel<PlayerModel>();
            m_enemyModel = this.GetModel<EnemyModel>();
            m_gameSystem = this.GetSystem<GameSystem>();

            m_enemyModel.OnEnemyDead.Register((position) =>
            {
                this.SendCommand(new DropExpCommand(position));
                this.SendCommand(new GetCoinCommand());
                this.SendCommand(new GetScoreCommand());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            //UI Init
            UIKit.OpenPanel<UIBeginPanel>();
            UIKit.OpenPanel<UIAchievePop>(UILevel.PopUI);


            //Audio Init
            this.GetModel<SettingModel>().AudioEnable.RegisterWithInitValue((audioEnable) =>
            {
                AudioKit.Settings.IsMusicOn.Value = audioEnable;
                AudioKit.Settings.IsSoundOn.Value = audioEnable;
            }); ;
            
            AudioKit.PlayMusic("Pretty Dungeon LOOP");
        }

        void Start()
        {
        }

        private void Update()
        {
#if UNITY_EDITOR

            //打印血量
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print($"maxhp: {m_playerModel.MaxHp}   hp: {m_playerModel.Hp}");
            }

            //开始游戏
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_gameSystem.GameStart();
            }

            //结束游戏
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_gameSystem.GameOver();
            }

            //暂停
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (m_gameModel.GameState == GameStates.isRunning)
                    this.GetSystem<GameSystem>().GamePause();
                else if (m_gameModel.GameState.Value == GameStates.isPaused)
                    this.GetSystem<GameSystem>().GameResume();
            }

            //经验++
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("经验加5");
                for(int i = 0; i < 5; i++)
                    this.SendCommand(new GetExpCommand());
            }

            //硬币++
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("硬币加100");
                GameArch.Interface.GetModel<ItemModel>().Coin.Value += 100;
            }

#endif

        }


        private void OnDestroy()
        {
            // 释放所有本脚本加载过的资源
            // 释放只是释放资源的引用
            // 当资源的引用数量为 0 时，会进行真正的资源卸载操作
            m_resLoader.Recycle2Cache();
            m_resLoader = null;
        }


    }
}
