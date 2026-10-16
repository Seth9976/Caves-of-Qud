using System;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000493 RID: 1171
	public class LoadingStatusWindow : SingletonWindowBase<LoadingStatusWindow>
	{
		// Token: 0x06002CF7 RID: 11511 RVA: 0x000F2971 File Offset: 0x000F0B71
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000F2974 File Offset: 0x000F0B74
		public void SetLoadingStatus(string text, bool waitForUiUpdate = false)
		{
			Task task = this.SetLoadingTextAsync(text);
			if (waitForUiUpdate)
			{
				while (!task.Wait(TimeSpan.FromMilliseconds(1000.0)))
				{
					if (Keyboard.Closed)
					{
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000F29AC File Offset: 0x000F0BAC
		public async Task SetLoadingTextAsync(string text)
		{
			if (text == null || this.StayHidden)
			{
				this.WantsToBeSeen = false;
			}
			else
			{
				this.WantsToBeSeen = true;
				await The.UiContext;
				this.StatusText.SetText(text);
				SingletonWindowBase<LoadingStatusWindow>.instance.Show();
				if (this.CurrentAlpha <= 0f)
				{
					this.StatusText.rectTransform.sizeDelta = new Vector2(this.StatusText.preferredWidth, 40f);
				}
				else
				{
					this.StatusText.rectTransform.sizeDelta = new Vector2(Math.Max(this.StatusText.rectTransform.sizeDelta.x, this.StatusText.preferredWidth), 40f);
				}
				this.CurrentAlpha = 1.5f;
				base.GetComponent<CanvasGroup>().alpha = 1f;
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000F29F8 File Offset: 0x000F0BF8
		public void Update()
		{
			if (this.StayHidden)
			{
				this.CurrentAlpha = 0f;
			}
			else if (!this.WantsToBeSeen && this.CurrentAlpha > 0f)
			{
				this.CurrentAlpha = Math.Max(0f, this.CurrentAlpha - Time.deltaTime * 2f);
			}
			base.GetComponent<CanvasGroup>().alpha = Math.Min(1f, this.CurrentAlpha);
			if (this.CurrentAlpha == 0f)
			{
				this.Hide();
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0400191C RID: 6428
		public bool StayHidden;

		// Token: 0x0400191D RID: 6429
		public UITextSkin StatusText;

		// Token: 0x0400191E RID: 6430
		public bool WantsToBeSeen;

		// Token: 0x0400191F RID: 6431
		private float CurrentAlpha;
	}
}
