using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:72b29bb7-b082-4248-83b2-98d00bbbe29d
	public partial class UIBeginPanel
	{
		public const string Name = "UIBeginPanel";
		
		[SerializeField]
		public UnityEngine.UI.Text DiamondNum;
		[SerializeField]
		public QFramework.Car.MyButton PlayBtn;
		[SerializeField]
		public QFramework.Car.MyButton StoreBtn;
		[SerializeField]
		public QFramework.Car.MyButton RankBtn;
		[SerializeField]
		public QFramework.Car.MyButton SettingBtn;
		
		private UIBeginPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			DiamondNum = null;
			PlayBtn = null;
			StoreBtn = null;
			RankBtn = null;
			SettingBtn = null;
			
			mData = null;
		}
		
		public UIBeginPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBeginPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBeginPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
