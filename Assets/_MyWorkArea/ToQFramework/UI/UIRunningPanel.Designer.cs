using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:d74371f5-425d-4443-8b09-03d255a8e855
	public partial class UIRunningPanel
	{
		public const string Name = "UIRunningPanel";
		
		[SerializeField]
		public RectTransform HpRoot;
		[SerializeField]
		public UnityEngine.UI.Text CoinNum;
		[SerializeField]
		public UnityEngine.UI.Slider ExpSlider;
		
		private UIRunningPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HpRoot = null;
			CoinNum = null;
			ExpSlider = null;
			
			mData = null;
		}
		
		public UIRunningPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIRunningPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIRunningPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
