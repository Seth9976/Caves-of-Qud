using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014E7 RID: 5351
	[ExecuteAlways]
	public class AutoSizeFromContentFitterWithMax : MonoBehaviour, ILayoutElement
	{
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600D126 RID: 53542 RVA: 0x004DA8F7 File Offset: 0x004D8AF7
		public float maxHeight
		{
			get
			{
				return (float)((double)Screen.height / Options.StageScale) - this.verticalSpaceReserved;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600D127 RID: 53543 RVA: 0x004DA90D File Offset: 0x004D8B0D
		public bool isScrolling
		{
			get
			{
				return this.maxHeight < this.contentHeight;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600D128 RID: 53544 RVA: 0x004DA91D File Offset: 0x004D8B1D
		public float minWidth
		{
			get
			{
				return this.contentWidth + (this.isScrolling ? this.extraWidthForScroll : 0f);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600D129 RID: 53545 RVA: 0x004DA93B File Offset: 0x004D8B3B
		public float preferredWidth
		{
			get
			{
				if (!this.preferredMode)
				{
					return this.contentWidth;
				}
				return -1f;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600D12A RID: 53546 RVA: 0x004DA951 File Offset: 0x004D8B51
		public float flexibleWidth
		{
			get
			{
				return (float)(this.preferredMode ? 1 : 0);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600D12B RID: 53547 RVA: 0x004DA960 File Offset: 0x004D8B60
		public float minHeight
		{
			get
			{
				if (!this.preferredMode)
				{
					return Math.Min(this.maxHeight, this.contentHeight);
				}
				return -1f;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x0600D12C RID: 53548 RVA: 0x004DA981 File Offset: 0x004D8B81
		public float preferredHeight
		{
			get
			{
				if (!this.preferredMode)
				{
					return -1f;
				}
				return Math.Min(this.maxHeight, this.contentHeight);
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600D12D RID: 53549 RVA: 0x004DA9A2 File Offset: 0x004D8BA2
		public float flexibleHeight
		{
			get
			{
				return (float)(this.preferredMode ? 1 : 0);
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600D12E RID: 53550 RVA: 0x004DA9B1 File Offset: 0x004D8BB1
		public int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D12F RID: 53551 RVA: 0x004DA9B4 File Offset: 0x004D8BB4
		public void CalculateLayoutInputHorizontal()
		{
			this.contentWidth = LayoutUtility.GetPreferredWidth(this.Content);
		}

		// Token: 0x0600D130 RID: 53552 RVA: 0x004DA9C7 File Offset: 0x004D8BC7
		public void CalculateLayoutInputVertical()
		{
			this.contentHeight = LayoutUtility.GetPreferredHeight(this.Content);
		}

		// Token: 0x04005279 RID: 21113
		public RectTransform Content;

		// Token: 0x0400527A RID: 21114
		public float verticalSpaceReserved;

		// Token: 0x0400527B RID: 21115
		public float extraWidthForScroll;

		// Token: 0x0400527C RID: 21116
		public float contentHeight;

		// Token: 0x0400527D RID: 21117
		public float contentWidth;

		// Token: 0x0400527E RID: 21118
		public bool preferredMode;
	}
}
