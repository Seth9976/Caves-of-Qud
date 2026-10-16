using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000041 RID: 65
	internal class ScrollViewState
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00011585 File Offset: 0x0000F785
		[RequiredByNativeCode]
		public ScrollViewState()
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001158F File Offset: 0x0000F78F
		public void ScrollTo(Rect pos)
		{
			this.ScrollTowards(pos, float.PositiveInfinity);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public bool ScrollTowards(Rect pos, float maxDelta)
		{
			Vector2 vector = this.ScrollNeeded(pos);
			bool flag = vector.sqrMagnitude < 0.0001f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = maxDelta == 0f;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = vector.magnitude > maxDelta;
					if (flag4)
					{
						vector = vector.normalized * maxDelta;
					}
					this.scrollPosition += vector;
					this.apply = true;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00011618 File Offset: 0x0000F818
		private Vector2 ScrollNeeded(Rect pos)
		{
			Rect rect = this.visibleRect;
			rect.x += this.scrollPosition.x;
			rect.y += this.scrollPosition.y;
			float num = pos.width - this.visibleRect.width;
			bool flag = num > 0f;
			if (flag)
			{
				pos.width -= num;
				pos.x += num * 0.5f;
			}
			num = pos.height - this.visibleRect.height;
			bool flag2 = num > 0f;
			if (flag2)
			{
				pos.height -= num;
				pos.y += num * 0.5f;
			}
			Vector2 zero = Vector2.zero;
			bool flag3 = pos.xMax > rect.xMax;
			if (flag3)
			{
				zero.x += pos.xMax - rect.xMax;
			}
			else
			{
				bool flag4 = pos.xMin < rect.xMin;
				if (flag4)
				{
					zero.x -= rect.xMin - pos.xMin;
				}
			}
			bool flag5 = pos.yMax > rect.yMax;
			if (flag5)
			{
				zero.y += pos.yMax - rect.yMax;
			}
			else
			{
				bool flag6 = pos.yMin < rect.yMin;
				if (flag6)
				{
					zero.y -= rect.yMin - pos.yMin;
				}
			}
			Rect rect2 = this.viewRect;
			rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
			rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
			zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
			zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
			return zero;
		}

		// Token: 0x0400013E RID: 318
		public Rect position;

		// Token: 0x0400013F RID: 319
		public Rect visibleRect;

		// Token: 0x04000140 RID: 320
		public Rect viewRect;

		// Token: 0x04000141 RID: 321
		public Vector2 scrollPosition;

		// Token: 0x04000142 RID: 322
		public bool apply;

		// Token: 0x04000143 RID: 323
		public bool isDuringTouchScroll;

		// Token: 0x04000144 RID: 324
		public Vector2 touchScrollStartMousePosition;

		// Token: 0x04000145 RID: 325
		public Vector2 touchScrollStartPosition;

		// Token: 0x04000146 RID: 326
		public Vector2 velocity;

		// Token: 0x04000147 RID: 327
		public float previousTimeSinceStartup;
	}
}
