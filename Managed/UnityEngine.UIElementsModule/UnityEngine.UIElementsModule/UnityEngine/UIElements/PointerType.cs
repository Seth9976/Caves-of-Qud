using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000286 RID: 646
	public static class PointerType
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		internal static string GetPointerType(int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId;
			string text;
			if (flag)
			{
				text = PointerType.mouse;
			}
			else
			{
				bool flag2 = pointerId == PointerId.penPointerIdBase;
				if (flag2)
				{
					text = PointerType.pen;
				}
				else
				{
					text = PointerType.touch;
				}
			}
			return text;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004CBF0 File Offset: 0x0004ADF0
		internal static bool IsDirectManipulationDevice(string pointerType)
		{
			return pointerType == PointerType.touch || pointerType == PointerType.pen;
		}

		// Token: 0x04000A76 RID: 2678
		public static readonly string mouse = "mouse";

		// Token: 0x04000A77 RID: 2679
		public static readonly string touch = "touch";

		// Token: 0x04000A78 RID: 2680
		public static readonly string pen = "pen";

		// Token: 0x04000A79 RID: 2681
		public static readonly string unknown = "";
	}
}
