using System;
using Genkit;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF1 RID: 2801
	public class SultanTowerDungeonSegment : SultanCircleDungeonSegment
	{
		// Token: 0x0600704D RID: 28749 RVA: 0x0027C53B File Offset: 0x0027A73B
		public SultanTowerDungeonSegment(Location2D pos, int radius, int thickness)
			: base(pos, radius)
		{
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x0027C54C File Offset: 0x0027A74C
		public override bool HasCustomColor(int x, int y)
		{
			Location2D location2D = Location2D.Get(x, y);
			Point2D point2D = new Point2D(location2D.X - this.center.X, location2D.Y - this.center.Y);
			point2D.x = (int)((float)point2D.x / 1.5f);
			return point2D.Distance(new Point2D(0, 0)) >= this.radius - this.thickness;
		}

		// Token: 0x040033B8 RID: 13240
		private int thickness = 2;
	}
}
