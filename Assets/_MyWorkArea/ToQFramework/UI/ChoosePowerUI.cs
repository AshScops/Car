using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace QFramework.Car
{
	public class ChoosePowerUIData : UIPanelData
	{
		public Queue<PowerData> CommonPowerQueue;
        public Queue<PowerData> SpecialPowerQueue;
    }

	public partial class ChoosePowerUI : UIPanel
	{
		private PowerData m_powerData = null;
		private Queue<GameObject> m_delQueue = new Queue<GameObject>();

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as ChoosePowerUIData ?? new ChoosePowerUIData();

            //��ȷ��Power
            this.ConfirmBtn.onClick.AddListener(() =>
			{
				if (m_powerData == null) return;

                GameArch.Interface.GetModel<PlayerModel>().PlayerPower.Add(m_powerData.PowerId);
				m_powerData = null;

                ActionKit.Sequence()
				.Custom(c =>
				{
					c.OnStart(() =>
					{
                        PanelRoot.DOFade(0, 0.3f).OnComplete(c.Finish);
                    });

                    c.OnFinish(() =>
                    {
                        GameArch.Interface.GetSystem<GameSystem>().GameResume();
                        this.CloseSelf();
                    });
                })
                .Start(GameController.Instance);
                
            });
        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
			if (uiData == null) return;

			ActionKit.Sequence()
				.Callback(()=>
				{
                    AudioKit.PlaySound("Special & Powerup (8)"); 

                    LevelUpDuration.gameObject.SetActive(true);
					PanelRoot.gameObject.SetActive(false);
                    PanelRoot.alpha = 0;
                })
				.Delay(1.0f)
                .Callback(() =>
				{
                    Queue<PowerData> commonQueue = (uiData as ChoosePowerUIData).CommonPowerQueue;
                    Queue<PowerData> specialQueue = (uiData as ChoosePowerUIData).SpecialPowerQueue;

                    if (commonQueue.Count == 0 && specialQueue.Count == 0)
                    {
                        CloseSelf();
                        return;
                    }

                    GeneratePowerGrid(commonQueue, CommonPowerBtnParent);
                    GeneratePowerGrid(specialQueue, SpecialPowerBtnParent);

                    LevelUpDuration.gameObject.SetActive(false);
                    PanelRoot.gameObject.SetActive(true);

					PanelRoot.DOFade(1, 0.3f);
                })
				.Start(GameController.Instance);
        }

        private void GeneratePowerGrid(Queue<PowerData> queue, Transform gridParent)
		{
            while (queue.Count > 0)
            {
                PowerData data = queue.Peek();
                queue.Dequeue();

                GameObject gridPrefab = ResLoader.Allocate().LoadSync<GameObject>("PowerGrid");
                GameObject grid = Instantiate(gridPrefab, gridParent);

                //��ѡ��Power
                grid.transform.Find("PowerBtn").GetComponent<Button>().onClick.AddListener(() =>
                {
                    m_powerData = data;
					this.PowerDesc.text = data.PowerDesc;
                });
                grid.transform.Find("PowerBtn").GetComponent<Image>().sprite = ResLoader.Allocate().LoadSync<Sprite>(data.PowerImg);
                grid.transform.Find("PowerName").GetComponent<Text>().text = data.PowerName;

                m_delQueue.Enqueue(grid);
            }
        }
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
			while(m_delQueue.Count > 0)
			{
				var go = m_delQueue.Peek();
                m_delQueue.Dequeue();
                if (go == null) continue;
				Destroy(go);
			}
		}
	}
}
