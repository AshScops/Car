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

            UIKit.OpenPanel("UIBeginPanel");
            AudioKit.Settings.IsMusicOn.Value = true;
            AudioKit.PlayMusic("Pretty Dungeon LOOP");
        }

        void Start()
        {
        }

        private void Update()
        {
#if UNITY_EDITOR

            //��ӡѪ��
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print($"maxhp: {m_playerModel.MaxHp}   hp: {m_playerModel.Hp}");
            }

            //��ʼ��Ϸ
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_gameSystem.GameStart();
            }

            //������Ϸ
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_gameSystem.GameOver();
            }

            //��ͣ
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (m_gameModel.GameState == GameStates.isRunning)
                    this.GetSystem<GameSystem>().GamePause();
                else if (m_gameModel.GameState.Value == GameStates.isPaused)
                    this.GetSystem<GameSystem>().GameResume();
            }

            //����++
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                print("�����5");
                for(int i = 0; i < 5; i++)
                    this.SendCommand(new GetExpCommand());
            }

            //Ӳ��++
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                print("Ӳ�Ҽ�100");
                GameArch.Interface.GetModel<ItemModel>().Coin.Value += 100;
            }

#endif

        }


        private void OnDestroy()
        {
            // �ͷ����б��ű����ع�����Դ
            // �ͷ�ֻ���ͷ���Դ������
            // ����Դ����������Ϊ 0 ʱ���������������Դж�ز���
            m_resLoader.Recycle2Cache();
            m_resLoader = null;
        }


    }
}
