using System;

namespace UnityEngine.UI
{
	// Token: 0x02000029 RID: 41
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000F558 File Offset: 0x0000D758
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000F560 File Offset: 0x0000D760
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F570 File Offset: 0x0000D770
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F57A File Offset: 0x0000D77A
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F584 File Offset: 0x0000D784
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
