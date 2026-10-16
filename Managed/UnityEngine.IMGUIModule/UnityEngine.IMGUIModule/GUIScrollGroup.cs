using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00011019 File Offset: 0x0000F219
		[RequiredByNativeCode]
		public GUIScrollGroup()
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011034 File Offset: 0x0000F234
		public override void CalcWidth()
		{
			float minWidth = this.minWidth;
			float maxWidth = this.maxWidth;
			bool flag = this.allowHorizontalScroll;
			if (flag)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			bool flag2 = this.allowHorizontalScroll;
			if (flag2)
			{
				bool flag3 = this.minWidth > 32f;
				if (flag3)
				{
					this.minWidth = 32f;
				}
				bool flag4 = minWidth != 0f;
				if (flag4)
				{
					this.minWidth = minWidth;
				}
				bool flag5 = maxWidth != 0f;
				if (flag5)
				{
					this.maxWidth = maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public override void SetHorizontal(float x, float width)
		{
			float num = (this.needsVerticalScrollbar ? (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left) : width);
			bool flag = this.allowHorizontalScroll && num < this.calcMinWidth;
			if (flag)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				bool flag2 = this.allowHorizontalScroll;
				if (flag2)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, num);
				this.rect.width = width;
				this.clientWidth = num;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000111DC File Offset: 0x0000F3DC
		public override void CalcHeight()
		{
			float minHeight = this.minHeight;
			float maxHeight = this.maxHeight;
			bool flag = this.allowVerticalScroll;
			if (flag)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			bool flag2 = this.needsHorizontalScrollbar;
			if (flag2)
			{
				float num = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += num;
				this.maxHeight += num;
			}
			bool flag3 = this.allowVerticalScroll;
			if (flag3)
			{
				bool flag4 = this.minHeight > 32f;
				if (flag4)
				{
					this.minHeight = 32f;
				}
				bool flag5 = minHeight != 0f;
				if (flag5)
				{
					this.minHeight = minHeight;
				}
				bool flag6 = maxHeight != 0f;
				if (flag6)
				{
					this.maxHeight = maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public override void SetVertical(float y, float height)
		{
			float num = height;
			bool flag = this.needsHorizontalScrollbar;
			if (flag)
			{
				num -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			bool flag2 = this.allowVerticalScroll && num < this.calcMinHeight;
			if (flag2)
			{
				bool flag3 = !this.needsHorizontalScrollbar && !this.needsVerticalScrollbar;
				if (flag3)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					bool flag4 = this.clientWidth < this.calcMinWidth;
					if (flag4)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float width = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = width;
				}
				float minHeight = this.minHeight;
				float maxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = minHeight;
				this.maxHeight = maxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				bool flag5 = this.allowVerticalScroll;
				if (flag5)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, num);
				this.rect.height = height;
				this.clientHeight = num;
			}
		}

		// Token: 0x0400012F RID: 303
		public float calcMinWidth;

		// Token: 0x04000130 RID: 304
		public float calcMaxWidth;

		// Token: 0x04000131 RID: 305
		public float calcMinHeight;

		// Token: 0x04000132 RID: 306
		public float calcMaxHeight;

		// Token: 0x04000133 RID: 307
		public float clientWidth;

		// Token: 0x04000134 RID: 308
		public float clientHeight;

		// Token: 0x04000135 RID: 309
		public bool allowHorizontalScroll = true;

		// Token: 0x04000136 RID: 310
		public bool allowVerticalScroll = true;

		// Token: 0x04000137 RID: 311
		public bool needsHorizontalScrollbar;

		// Token: 0x04000138 RID: 312
		public bool needsVerticalScrollbar;

		// Token: 0x04000139 RID: 313
		public GUIStyle horizontalScrollbar;

		// Token: 0x0400013A RID: 314
		public GUIStyle verticalScrollbar;
	}
}
