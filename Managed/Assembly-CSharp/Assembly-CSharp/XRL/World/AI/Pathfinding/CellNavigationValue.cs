using System;

namespace XRL.World.AI.Pathfinding
{
	// Token: 0x02000B72 RID: 2930
	[Serializable]
	public class CellNavigationValue : IComparable
	{
		// Token: 0x06007569 RID: 30057 RVA: 0x002C00AF File Offset: 0x002BE2AF
		public CellNavigationValue(double Cost, double Estimate, Cell C, Cell Parent, string Direction)
		{
			this.Cost = Cost;
			this.Estimate = Estimate;
			this.Total = this.Cost + this.Estimate;
			this.C = C;
			this.Parent = Parent;
			this.Direction = Direction;
		}

		// Token: 0x0600756A RID: 30058 RVA: 0x002C00EF File Offset: 0x002BE2EF
		public CellNavigationValue Set(double Cost = 0.0, double Estimate = 0.0, Cell C = null, Cell Parent = null, string Direction = null)
		{
			this.Cost = Cost;
			this.Estimate = Estimate;
			this.Total = this.Cost + this.Estimate;
			this.C = C;
			this.Parent = Parent;
			this.Direction = Direction;
			return this;
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x002C012A File Offset: 0x002BE32A
		public int CompareTo(object Nav2)
		{
			return this.Total.CompareTo((Nav2 as CellNavigationValue).Total);
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x002C0142 File Offset: 0x002BE342
		public int CompareTo(CellNavigationValue Nav2)
		{
			return this.Total.CompareTo(Nav2.Total);
		}

		// Token: 0x040035AB RID: 13739
		public Cell C;

		// Token: 0x040035AC RID: 13740
		public Cell Parent;

		// Token: 0x040035AD RID: 13741
		public string Direction;

		// Token: 0x040035AE RID: 13742
		public double Cost;

		// Token: 0x040035AF RID: 13743
		public double Estimate;

		// Token: 0x040035B0 RID: 13744
		public double Total;
	}
}
