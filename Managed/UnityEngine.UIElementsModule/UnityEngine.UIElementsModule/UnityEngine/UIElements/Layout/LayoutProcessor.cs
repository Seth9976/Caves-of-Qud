using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000696 RID: 1686
	internal static class LayoutProcessor
	{
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000BB991 File Offset: 0x000B9B91
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000BB998 File Offset: 0x000B9B98
		public static ILayoutProcessor Processor
		{
			get
			{
				return LayoutProcessor.s_Processor;
			}
			set
			{
				LayoutProcessor.s_Processor = value ?? new LayoutProcessorNative();
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000BB9A9 File Offset: 0x000B9BA9
		public static void CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection)
		{
			LayoutProcessor.s_Processor.CalculateLayout(node, parentWidth, parentHeight, parentDirection);
		}

		// Token: 0x04001835 RID: 6197
		private static ILayoutProcessor s_Processor = new LayoutProcessorNative();
	}
}
