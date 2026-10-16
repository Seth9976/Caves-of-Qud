using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x020004A0 RID: 1184
	[ExecuteAlways]
	public class HPBar : MonoBehaviour
	{
		// Token: 0x06002D5E RID: 11614 RVA: 0x000F5674 File Offset: 0x000F3874
		public void UpdateBar()
		{
			this.Bar.anchorMax = new Vector2(Math.Min(1f, (float)(this.BarValue - this.BarStart) / (float)(this.BarEnd - this.BarStart)), 1f);
			this.Bar.GetComponent<Image>().color = this.BarColor;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000F56D3 File Offset: 0x000F38D3
		public void SetText(string t)
		{
			this.text.SetText(t);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000F56E2 File Offset: 0x000F38E2
		public void Update()
		{
			if (this.WantsUpdate)
			{
				this.UpdateBar();
				this.WantsUpdate = false;
			}
		}

		// Token: 0x04001972 RID: 6514
		public RectTransform Bar;

		// Token: 0x04001973 RID: 6515
		public Color BarColor;

		// Token: 0x04001974 RID: 6516
		public int BarStart;

		// Token: 0x04001975 RID: 6517
		public int BarEnd = 100;

		// Token: 0x04001976 RID: 6518
		public int BarValue = 50;

		// Token: 0x04001977 RID: 6519
		public bool WantsUpdate;

		// Token: 0x04001978 RID: 6520
		public UITextSkin text;
	}
}
