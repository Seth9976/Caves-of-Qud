using System;

namespace EnhancedUI.EnhancedScroller
{
	// Token: 0x020001DE RID: 478
	public interface IEnhancedScrollerDelegate
	{
		// Token: 0x06001137 RID: 4407
		int GetNumberOfCells(EnhancedScroller scroller);

		// Token: 0x06001138 RID: 4408
		float GetCellViewSize(EnhancedScroller scroller, int dataIndex);

		// Token: 0x06001139 RID: 4409
		EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex);
	}
}
