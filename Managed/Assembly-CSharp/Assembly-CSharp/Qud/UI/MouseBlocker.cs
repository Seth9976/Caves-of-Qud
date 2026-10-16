using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200041F RID: 1055
	[UIView("MouseBlocker", false, false, false, null, null, false, 0, false, UICanvas = "MouseBlocker", UICanvasHost = 1)]
	public class MouseBlocker : SingletonWindowBase<MouseBlocker>, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06002804 RID: 10244 RVA: 0x000CF010 File Offset: 0x000CD210
		public async void UpdateOptions()
		{
			await The.UiContext;
			if (Options.MouseInput)
			{
				this.Hide();
				base.gameObject.SetActive(false);
				this._canvas.enabled = false;
				this._raycaster.enabled = false;
			}
			else
			{
				base.raycaster.enabled = true;
				base.takesInput = true;
				this.Show();
				base.canvas.enabled = true;
				if (this.blocker != null)
				{
					this.blocker.enabled = true;
				}
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000CF047 File Offset: 0x000CD247
		public override bool AllowPassthroughInput()
		{
			return true;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000CF04C File Offset: 0x000CD24C
		public void Update()
		{
			if (!base.canvas.enabled || !base.raycaster.enabled)
			{
				base.canvas.enabled = true;
				base.raycaster.enabled = true;
			}
			if (this.clickCount > 0 && this.clickReset < Time.realtimeSinceStartup)
			{
				this.clickCount = 0;
			}
			if (base.transform.GetSiblingIndex() != base.transform.parent.childCount - 1)
			{
				base.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000CF0D4 File Offset: 0x000CD2D4
		public async void OnPointerClick(PointerEventData eventData)
		{
			this.clickReset = Time.realtimeSinceStartup + 60f;
			this.clickCount++;
			if (this.clickCount >= 5)
			{
				if (this.blocker == null)
				{
					this.blocker = base.GetComponent<Image>();
				}
				this.blocker.enabled = false;
				bool flag = await Popup.ShowYesNoAsync("Mouse input is disabled but you clicked on the screen several times. Would you like to enable mouse input?") != DialogResult.Yes;
				this.blocker.enabled = true;
				if (!flag)
				{
					Options.SetOption("OptionMouseInput", true);
				}
			}
		}

		// Token: 0x040013DA RID: 5082
		private Image blocker;

		// Token: 0x040013DB RID: 5083
		private int clickCount;

		// Token: 0x040013DC RID: 5084
		private float clickReset;
	}
}
