using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200043D RID: 1085
	[ExecuteInEditMode]
	public class KeybindBox : MonoBehaviour
	{
		// Token: 0x06002935 RID: 10549 RVA: 0x000D66F7 File Offset: 0x000D48F7
		private void Start()
		{
			this.forceUpdate = true;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000D6700 File Offset: 0x000D4900
		public void Apply()
		{
			this.forceUpdate = true;
			this.Update();
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000D6710 File Offset: 0x000D4910
		public void Update()
		{
			NavigationContext navigationContext = this.context.context;
			bool? flag = ((navigationContext != null) ? new bool?(navigationContext.IsActive(true)) : null);
			bool? flag2 = this.wasSelected;
			bool? flag3 = flag;
			if ((flag2.GetValueOrDefault() == flag3.GetValueOrDefault()) & (flag2 != null == (flag3 != null)))
			{
				flag3 = this.lastEditMode;
				bool flag4 = this.editMode;
				if (((flag3.GetValueOrDefault() == flag4) & (flag3 != null)) && !this.forceUpdate)
				{
					return;
				}
			}
			this.lastEditMode = new bool?(this.editMode);
			this.wasSelected = flag;
			this.forceUpdate = false;
			this.textSkin.text = this.boxText;
			this.selectedMode = flag.GetValueOrDefault();
			bool flag5 = this.selectedMode || this.editMode;
			Color color = (flag5 ? new Color(this.fullBorderImage.color.r, this.fullBorderImage.color.g, this.fullBorderImage.color.b, 1f) : new Color(this.fullBorderImage.color.r, this.fullBorderImage.color.g, this.fullBorderImage.color.b, 0f));
			if (this.fullBorderImage.color != color)
			{
				this.fullBorderImage.color = color;
			}
			color = (flag5 ? new Color(this.backgroundImage.color.r, this.backgroundImage.color.g, this.backgroundImage.color.b, 1f) : new Color(this.backgroundImage.color.r, this.backgroundImage.color.g, this.backgroundImage.color.b, 0f));
			if (this.backgroundImage.color != color)
			{
				this.backgroundImage.color = color;
			}
			if (this.editMode)
			{
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
				{
					this.textSkin.text = "{{R|...}}";
				}
				else
				{
					this.textSkin.text = "{{R|press key...}}";
				}
			}
			this.textSkin.Apply();
		}

		// Token: 0x040014FB RID: 5371
		public Image backgroundImage;

		// Token: 0x040014FC RID: 5372
		public Image fullBorderImage;

		// Token: 0x040014FD RID: 5373
		public UITextSkin textSkin;

		// Token: 0x040014FE RID: 5374
		public FrameworkContext context;

		// Token: 0x040014FF RID: 5375
		public bool editMode;

		// Token: 0x04001500 RID: 5376
		public bool selectedMode;

		// Token: 0x04001501 RID: 5377
		public bool forceUpdate;

		// Token: 0x04001502 RID: 5378
		public string boxText = "{{c|None}}";

		// Token: 0x04001503 RID: 5379
		private bool? lastEditMode;

		// Token: 0x04001504 RID: 5380
		private bool? wasSelected;
	}
}
