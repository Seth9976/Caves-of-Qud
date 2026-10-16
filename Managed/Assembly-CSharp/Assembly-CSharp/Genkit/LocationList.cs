using System;
using System.Collections.Generic;
using System.Linq;

namespace Genkit
{
	// Token: 0x02000379 RID: 889
	public class LocationList : ILocationArea
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x000A4450 File Offset: 0x000A2650
		public LocationList(IEnumerable<Location2D> area)
		{
			this.locations = area;
			int num = int.MaxValue;
			int num2 = int.MinValue;
			int num3 = int.MaxValue;
			int num4 = int.MinValue;
			foreach (Location2D location2D in this.locations)
			{
				if (location2D.X < num)
				{
					num = location2D.X;
				}
				if (location2D.Y < num3)
				{
					num3 = location2D.Y;
				}
				if (location2D.X > num2)
				{
					num2 = location2D.X;
				}
				if (location2D.Y > num4)
				{
					num4 = location2D.Y;
				}
			}
			int num5 = (num2 + num) / 2;
			int num6 = (num4 + num3) / 2;
			Location2D location2D2 = Location2D.Get(num5, num6);
			if (location2D2 != null)
			{
				int num7 = int.MaxValue;
				foreach (Location2D location2D3 in this.locations)
				{
					int num8 = location2D3.Distance(location2D2);
					if (num8 < num7)
					{
						this.center = location2D3;
						num7 = num8;
					}
				}
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000A4594 File Offset: 0x000A2794
		public bool PointIn(Location2D location)
		{
			return this.locations.Contains(location);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000A45A2 File Offset: 0x000A27A2
		public IEnumerable<Location2D> EnumerateLocations()
		{
			return this.locations;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000A45AC File Offset: 0x000A27AC
		public void generateBorders()
		{
			foreach (Location2D location2D in this.locations)
			{
				if (location2D.cardinalNeighbors.Count<Location2D>((Location2D n) => this.locations.Contains(n)) == 4)
				{
					this.nonborder.Add(location2D);
				}
				else
				{
					this.border.Add(location2D);
				}
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000A4628 File Offset: 0x000A2828
		public IEnumerable<Location2D> EnumerateBorderLocations()
		{
			if (this.border == null)
			{
				this.generateBorders();
			}
			return this.border;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000A463E File Offset: 0x000A283E
		public IEnumerable<Location2D> EnumerateNonBorderLocations()
		{
			if (this.nonborder == null)
			{
				this.generateBorders();
			}
			return this.nonborder;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000A4654 File Offset: 0x000A2854
		public Location2D GetCenter()
		{
			return this.center;
		}

		// Token: 0x040010BD RID: 4285
		private IEnumerable<Location2D> locations;

		// Token: 0x040010BE RID: 4286
		private List<Location2D> border;

		// Token: 0x040010BF RID: 4287
		private List<Location2D> nonborder;

		// Token: 0x040010C0 RID: 4288
		private Location2D center = Location2D.Get(0, 0);
	}
}
