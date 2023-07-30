using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace QFramework.Car
{
	public class UIBeginPanelData : UIPanelData
	{
	}
	public partial class UIBeginPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBeginPanelData ?? new UIBeginPanelData();

            GameArch.Interface.GetModel<ItemModel>().Diamond.RegisterWithInitValue((diamondCnt) =>
            {
                DiamondNum.text = diamondCnt.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            PlayBtn.onClick.AddListener(() =>
            {
                PlayBtn.interactable = false;

                ActionKit.Sequence()
						.Callback(() =>
						{
							//按钮动效
                            PlayBtn.transform.DOScale(1.3f, 0.3f)
                                            .SetEase(Ease.OutQuad)
                                            .SetLoops(2, LoopType.Yoyo);
                        })
						.Delay(0.15f)
						.Callback(() =>
                        {
							//界面渐隐+关闭界面
                            ActionKit.Custom(c =>
                            {
                                c.OnStart(() => { this.GetComponent<CanvasGroup>().DOFade(0, 0.5f); });
                                c.OnFinish(() => { CloseSelf(); });

                            }).Start(GameController.Instance);

                        })
                        .Delay(0.1f)
                        .Callback(() =>
                        {
                            //游戏开始
                            GameArch.Interface.GetSystem<GameSystem>().GameStart();
                        })
                        .Start(GameController.Instance);

            });

            //TODO:
            StoreBtn.onClick.AddListener(() =>
            {
                UIKit.OpenPanel("UISkillTreePanel"); 
            });

            //TODO:
            RankBtn.onClick.AddListener(() =>
            {


            });

			//TODO:
            SettingBtn.onClick.AddListener(() =>
            {

            });


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
