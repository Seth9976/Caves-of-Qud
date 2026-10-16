using System;
using Qud.UI;
using UnityEngine;

namespace XRL.UI
{
	// Token: 0x0200147B RID: 5243
	public class TitledIconButton : MonoBehaviour
	{
		// Token: 0x0600CE86 RID: 52870 RVA: 0x004B2084 File Offset: 0x004B0284
		public void SetTitle(string title)
		{
			UITextSkin titleText = this.TitleText;
			if (titleText == null)
			{
				return;
			}
			this.Title = title;
			titleText.SetText(title);
		}

		// Token: 0x0600CE87 RID: 52871 RVA: 0x004B20AC File Offset: 0x004B02AC
		public void Update()
		{
			if (this.TitleText != null && this.TitleText.text != this.Title)
			{
				this.TitleText.SetText(this.Title);
			}
		}

		// Token: 0x0400501D RID: 20509
		public UITextSkin TitleText;

		// Token: 0x0400501E RID: 20510
		public string Title;

		// Token: 0x0400501F RID: 20511
		public ImageTinyFrame ImageTinyFrame;
	}
}
