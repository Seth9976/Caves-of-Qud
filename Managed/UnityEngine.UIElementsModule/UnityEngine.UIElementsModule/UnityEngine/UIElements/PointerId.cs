using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000287 RID: 647
	public static class PointerId
	{
		// Token: 0x04000A7A RID: 2682
		public static readonly int maxPointers = 32;

		// Token: 0x04000A7B RID: 2683
		public static readonly int invalidPointerId = -1;

		// Token: 0x04000A7C RID: 2684
		public static readonly int mousePointerId = 0;

		// Token: 0x04000A7D RID: 2685
		public static readonly int touchPointerIdBase = 1;

		// Token: 0x04000A7E RID: 2686
		public static readonly int touchPointerCount = 20;

		// Token: 0x04000A7F RID: 2687
		public static readonly int penPointerIdBase = PointerId.touchPointerIdBase + PointerId.touchPointerCount;

		// Token: 0x04000A80 RID: 2688
		public static readonly int penPointerCount = 2;

		// Token: 0x04000A81 RID: 2689
		internal static readonly int[] hoveringPointers = new int[] { PointerId.mousePointerId };
	}
}
