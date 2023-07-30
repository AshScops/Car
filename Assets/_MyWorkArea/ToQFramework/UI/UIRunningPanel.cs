using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

namespace QFramework.Car
{
	public class UIRunningPanelData : UIPanelData
	{
	}
	public partial class UIRunningPanel : UIPanel
	{
		private PlayerModel m_playerModel;
        private ItemModel m_itemModel;
        private List<GameObject> m_hpList = new List<GameObject>();

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIRunningPanelData ?? new UIRunningPanelData();
			m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
            m_itemModel = GameArch.Interface.GetModel<ItemModel>();

            m_playerModel.MaxHp.RegisterWithInitValue((maxhp) =>
            {
                while(m_hpList.Count < maxhp)
                {
                    m_hpList.Add(ResUtil.GenerateGO("HpGrid", HpRoot));
                }
            }).UnRegisterWhenGameObjectDestroyed(this);

            m_playerModel.Hp.RegisterWithInitValue((hp) =>
            {
				if (hp < 0) return;

                var texture = ResUtil.LoadTexture2D("heart");
                Sprite heart = Sprite.Create(texture, new Rect(0f, 96f, 32f, 32f), new Vector2(0.5f, 0.5f));
                Sprite greyHeart = Sprite.Create(texture, new Rect(64f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));

                for (int i = 0; i < m_hpList.Count; i++)
				{
					if(i < hp)
                        m_hpList[i].GetComponent<Image>().sprite = heart;
                    else
                        m_hpList[i].GetComponent<Image>().sprite = greyHeart;
                }

            }).UnRegisterWhenGameObjectDestroyed(this);

            m_playerModel.CurrentExp.RegisterWithInitValue((exp) =>
			{
                //Debug.Log("exp:" + m_playerModel.CurrentExp + " limit:" + m_playerModel.LevelUpExpUpperLimit);
				float v = 1.0f * m_playerModel.CurrentExp / m_playerModel.LevelUpExpUpperLimit;
				v = v > 1f ? 1f : v;
				ExpSlider.value = v;
			}).UnRegisterWhenGameObjectDestroyed(this);


			m_itemModel.Coin.RegisterWithInitValue((coin) =>
			{
				CoinNum.text = coin.ToString();
			}).UnRegisterWhenGameObjectDestroyed(this);

        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
