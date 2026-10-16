using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001E RID: 30
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000E061 File Offset: 0x0000C261
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E069 File Offset: 0x0000C269
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000E079 File Offset: 0x0000C279
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E083 File Offset: 0x0000C283
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000E08D File Offset: 0x0000C28D
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
