using System;

namespace Genkit
{
	// Token: 0x02000389 RID: 905
	[Serializable]
	public class CellNavigationValue : IComparable
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x000AA3B2 File Offset: 0x000A85B2
		public CellNavigationValue(int g, int h, PathfinderNode _Cell, PathfinderNode pParent, string pDirection)
		{
			this.StartToNode = g;
			this.cCell = _Cell;
			this.EstimatedNodeToGoal = h;
			this.EstimatedTotalCost = g + h;
			this.Parent = pParent;
			this.Direction = pDirection;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000AA3E8 File Offset: 0x000A85E8
		public CellNavigationValue Set(int g, int h, PathfinderNode _Cell, PathfinderNode pParent, string pDirection)
		{
			this.StartToNode = g;
			this.cCell = _Cell;
			this.EstimatedNodeToGoal = h;
			this.EstimatedTotalCost = g + h;
			this.Parent = pParent;
			this.Direction = pDirection;
			return this;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000AA419 File Offset: 0x000A8619
		public int CompareTo(object Nav2)
		{
			return this.EstimatedTotalCost.CompareTo((Nav2 as CellNavigationValue).EstimatedTotalCost);
		}

		// Token: 0x04001104 RID: 4356
		public PathfinderNode cCell;

		// Token: 0x04001105 RID: 4357
		public PathfinderNode Parent;

		// Token: 0x04001106 RID: 4358
		public string Direction;

		// Token: 0x04001107 RID: 4359
		public int StartToNode;

		// Token: 0x04001108 RID: 4360
		public int EstimatedNodeToGoal;

		// Token: 0x04001109 RID: 4361
		public int EstimatedTotalCost;
	}
}
