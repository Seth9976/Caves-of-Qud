using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200033D RID: 829
	internal struct Spacing
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
		public float horizontal
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0005CF18 File Offset: 0x0005B118
		public float vertical
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0005CF37 File Offset: 0x0005B137
		public Spacing(float left, float top, float right, float bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005CF58 File Offset: 0x0005B158
		public static Rect operator +(Rect r, Spacing a)
		{
			r.x -= a.left;
			r.y -= a.top;
			r.width += a.horizontal;
			r.height += a.vertical;
			return r;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0005CFC4 File Offset: 0x0005B1C4
		public static Rect operator -(Rect r, Spacing a)
		{
			r.x += a.left;
			r.y += a.top;
			r.width = Mathf.Max(0f, r.width - a.horizontal);
			r.height = Mathf.Max(0f, r.height - a.vertical);
			return r;
		}

		// Token: 0x04000D23 RID: 3363
		public float left;

		// Token: 0x04000D24 RID: 3364
		public float top;

		// Token: 0x04000D25 RID: 3365
		public float right;

		// Token: 0x04000D26 RID: 3366
		public float bottom;
	}
}
