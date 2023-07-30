using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:092fab00-05bc-44d9-9ec3-355b7f2f5677
	public partial class UIEndPanel
	{
		public const string Name = "UIEndPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform EndPanelRoot;
		[SerializeField]
		public UnityEngine.RectTransform Title;
		[SerializeField]
		public UnityEngine.UI.Text Score;
		[SerializeField]
		public UnityEngine.UI.Text Level;
		[SerializeField]
		public UnityEngine.UI.Text SumDmg;
		[SerializeField]
		public QFramework.Car.MyButton OkBtn;
		
		private UIEndPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EndPanelRoot = null;
			Title = null;
			Score = null;
			Level = null;
			SumDmg = null;
			OkBtn = null;
			
			mData = null;
		}
		
		public UIEndPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIEndPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIEndPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
