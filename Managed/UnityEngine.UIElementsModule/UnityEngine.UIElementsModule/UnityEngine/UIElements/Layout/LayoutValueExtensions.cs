using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006C5 RID: 1733
	internal static class LayoutValueExtensions
	{
		// Token: 0x06003219 RID: 12825 RVA: 0x000BD2F4 File Offset: 0x000BB4F4
		public static LayoutValue Percent(this float value)
		{
			return LayoutValue.Percent(value);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000BD30C File Offset: 0x000BB50C
		public static LayoutValue Pt(this float value)
		{
			return LayoutValue.Point(value);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000BD324 File Offset: 0x000BB524
		public static LayoutValue Percent(this int value)
		{
			return LayoutValue.Percent((float)value);
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000BD340 File Offset: 0x000BB540
		public static LayoutValue Pt(this int value)
		{
			return LayoutValue.Point((float)value);
		}
	}
}
