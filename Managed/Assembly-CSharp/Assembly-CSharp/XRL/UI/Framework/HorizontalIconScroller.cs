using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x02001516 RID: 5398
	public class HorizontalIconScroller : HorizontalScroller
	{
		// Token: 0x0600D23F RID: 53823 RVA: 0x004E0D3C File Offset: 0x004DEF3C
		public void checkSafeArea()
		{
			if (this.safeArea == null)
			{
				return;
			}
			if (Media.sizeClass != this.lastClass)
			{
				this.lastClass = Media.sizeClass;
				if (Media.sizeClass < Media.SizeClass.Medium)
				{
					this.safeArea.anchoredPosition = new Vector2(0f, 0f);
					this.safeArea.sizeDelta = new Vector2(0f, -150f);
				}
				else
				{
					this.safeArea.anchoredPosition = new Vector2(0f, -25f);
					this.safeArea.sizeDelta = new Vector2(-300f, -250f);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.safeArea);
			}
		}

		// Token: 0x0600D240 RID: 53824 RVA: 0x004E0DF1 File Offset: 0x004DEFF1
		public override void Awake()
		{
			this.checkSafeArea();
			base.Awake();
		}

		// Token: 0x0600D241 RID: 53825 RVA: 0x004E0DFF File Offset: 0x004DEFFF
		public override void Update()
		{
			this.checkSafeArea();
			base.Update();
		}

		// Token: 0x04005355 RID: 21333
		public GameObject gridContainer;

		// Token: 0x04005356 RID: 21334
		public RectTransform safeArea;

		// Token: 0x04005357 RID: 21335
		private Media.SizeClass lastClass = Media.SizeClass.Unset;
	}
}
