using System;
using UnityEngine;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x020004A4 RID: 1188
	[ExecuteAlways]
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollViewContentFitter : LayoutElement
	{
		// Token: 0x06002D90 RID: 11664 RVA: 0x000F9170 File Offset: 0x000F7370
		public void Update()
		{
			this._sr = base.GetComponent<ScrollRect>();
			this.preferredWidth = Math.Min((float)this.MaxWidth, this._sr.content.rect.width + this._sr.verticalScrollbarSpacing);
			this.preferredHeight = Math.Min((float)this.MaxHeight, this._sr.content.rect.height) + this._sr.horizontalScrollbarSpacing;
		}

		// Token: 0x040019D3 RID: 6611
		public int MaxHeight = 600;

		// Token: 0x040019D4 RID: 6612
		public int MaxWidth = 800;

		// Token: 0x040019D5 RID: 6613
		private ScrollRect _sr;
	}
}
