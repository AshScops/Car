using UnityEngine;
using System;
using System.Collections;

namespace QFramework.Car
{
    public partial class Player : ViewController, IController
	{
        private const float TOUCH_Y_LOW_EDGE = 0.1f;
        private const float TOUCH_Y_HIGH_EDGE = 0.9f;

        private GameModel m_gameModel;
        private PlayerModel m_playerModel;

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        void Awake()
        {
            m_gameModel = this.GetModel<GameModel>();
            m_playerModel = this.GetModel<PlayerModel>();

        }

        private void Start()
        {
            ///监听CurrentExp
            m_playerModel.CurrentExp.RegisterWithInitValue(currentExp =>
            {
                if (currentExp >= m_playerModel.LevelUpExpUpperLimit)
                {
                    m_playerModel.LevelUpExpUpperLimit.Value = 10 * m_playerModel.CurrentLevel;
                    m_playerModel.CurrentLevel.Value++;
                    m_playerModel.CurrentExp.Value = 0;
                    this.SendCommand(new ShowLevelUpUICommand());
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ///监听CurrentLevel
            m_playerModel.CurrentLevel.Register(playerCurrentLevel =>
            {
                print("更新LevelUI");

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

        }

        private void Update()
        {
            if (m_gameModel.GameState != GameStates.isRunning) return;
            if (Camera.main == null) return;

            if (Input.GetMouseButton(0))
            {
                Vector3 touchScreenPos = Input.mousePosition;
                //Vector3 viewportPos = Camera.main.ScreenToViewportPoint(touchScreenPos);
                //if (viewportPos.y <= TOUCH_Y_LOW_EDGE || viewportPos.y >= TOUCH_Y_HIGH_EDGE) return;

                Ray ray = Camera.main.ScreenPointToRay(touchScreenPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 direction = hit.point - this.transform.position;
                    direction.y = 0;
                    Move(this.transform, direction, m_playerModel.MoveSpeed, m_playerModel.RotSpeed);
                }
            }

            m_playerModel.OnUpdate();
            this.SendCommand(new CollectDropCommand());
        }

        public void Move(Transform objectTrans, Vector3 direction, float moveSpeed, float rotSpeed)
        {
            direction = direction.normalized;
            //位移
            objectTrans.position += direction * moveSpeed * Time.deltaTime;

            //旋转
            Quaternion targetRot = Quaternion.LookRotation(direction);
            objectTrans.rotation = Quaternion.Slerp(objectTrans.rotation,
                targetRot, rotSpeed * Time.deltaTime);

            //限制只沿Y轴旋转
            objectTrans.rotation = Quaternion.Euler(0, objectTrans.eulerAngles.y, 0);
        }

        public IEnumerator HitFlash(float hurtCd)
        {
            var cd = hurtCd;
            var flashDuration = 0.1f;
            do
            {
                if (m_gameModel.GameState != GameStates.isRunning)
                {
                    yield return null;
                    continue;
                }

                Body.material.SetFloat("_ColorRange", 1f);
                yield return new WaitForSeconds(flashDuration);
                Body.material.SetFloat("_ColorRange", 0f);
                yield return new WaitForSeconds(flashDuration);

                cd -= Time.deltaTime + 2 * flashDuration;
            }
            while (cd > 0);
        }
    }
}
