using System;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000427 RID: 1063
	[ExecuteAlways]
	public class SelectableTextMenuItem : ControlledSelectable
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000D02F0 File Offset: 0x000CE4F0
		public string itemText
		{
			get
			{
				if (!(this.data is QudMenuItem?))
				{
					return null;
				}
				QudMenuItem? qudMenuItem;
				return qudMenuItem.GetValueOrDefault().text;
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000D0328 File Offset: 0x000CE528
		public IRenderable getIcon()
		{
			if (!(this.data is QudMenuItem?))
			{
				return null;
			}
			QudMenuItem? qudMenuItem;
			return qudMenuItem.GetValueOrDefault().icon;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000D035D File Offset: 0x000CE55D
		public override void Update()
		{
			if (this.oldItemText != this.itemText)
			{
				this.oldItemText = this.itemText;
				this.SelectChanged(base.selected);
			}
			base.Update();
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000D0390 File Offset: 0x000CE590
		public override void SelectChanged(bool newState)
		{
			base.SelectChanged(newState);
			IRenderable icon = this.getIcon();
			this.iconImage.gameObject.SetActive(icon != null);
			if (icon != null)
			{
				this.iconImage.FromRenderable(icon, true);
			}
			if (newState)
			{
				this.cursor.text = RTF.FormatToRTF("&W>", "FF", -1, false);
				this.item.useBlockWrap = true;
				this.item.blockWrap = this.maxTextWidth;
				this.item.SetText("{{W|" + this.itemText + "}}");
			}
			else
			{
				this.cursor.text = " ";
				this.item.useBlockWrap = true;
				this.item.blockWrap = this.maxTextWidth;
				this.item.SetText("{{c|" + this.itemText + "}}");
			}
			if (TutorialManager.currentStep != null)
			{
				QudMenuItem? qudMenuItem;
				ControlId.Assign(base.gameObject, "QudTextMenuItem:" + ((this.data is QudMenuItem?) ? qudMenuItem.GetValueOrDefault().simpleText : null), false);
			}
		}

		// Token: 0x04001405 RID: 5125
		public RectTransform progressBar;

		// Token: 0x04001406 RID: 5126
		public global::UnityEngine.UI.Text cursor;

		// Token: 0x04001407 RID: 5127
		public UITextSkin item;

		// Token: 0x04001408 RID: 5128
		public UIThreeColorProperties iconImage;

		// Token: 0x04001409 RID: 5129
		public int maxTextWidth = 78;

		// Token: 0x0400140A RID: 5130
		[NonSerialized]
		private string oldItemText;
	}
}
