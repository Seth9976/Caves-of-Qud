using System;
using Genkit;
using UnityEngine;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AEE RID: 2798
	public class SultanCircleDungeonSegment : ISultanDungeonSegment
	{
		// Token: 0x0600702C RID: 28716 RVA: 0x0027C168 File Offset: 0x0027A368
		public SultanCircleDungeonSegment(Location2D pos, int radius)
		{
			this.center = pos;
			this.radius = radius;
		}

		// Token: 0x0600702D RID: 28717 RVA: 0x0027C17E File Offset: 0x0027A37E
		public override int width()
		{
			return (int)(2.0 * ((double)this.radius * 1.5)) + 1;
		}

		// Token: 0x0600702E RID: 28718 RVA: 0x0027C19E File Offset: 0x0027A39E
		public override int height()
		{
			return 2 * this.radius + 1;
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x0027C1AA File Offset: 0x0027A3AA
		public override bool HasCustomColor(int x, int y)
		{
			return false;
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x0027C1AD File Offset: 0x0027A3AD
		public override Color32 GetCustomColor(int x, int y)
		{
			return ColorOutputMap.BLACK;
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x0027C1B4 File Offset: 0x0027A3B4
		public override bool contains(int x, int y)
		{
			return this.contains(Location2D.Get(x, y));
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x0027C1C4 File Offset: 0x0027A3C4
		public override bool contains(Location2D location)
		{
			double num = (double)this.radius * 1.5;
			double num2 = (double)this.radius;
			if (num <= 0.0 || num2 <= 0.0)
			{
				return false;
			}
			Point point = new Point(location.X - this.center.X, location.Y - this.center.Y);
			return (double)(point.X * point.X) / (num * num) + (double)(point.Y * point.Y) / (num2 * num2) <= 1.0;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06007033 RID: 28723 RVA: 0x0027C262 File Offset: 0x0027A462
		public override int x1
		{
			get
			{
				return Math.Min(79, Math.Max(0, this.center.X - (int)((double)this.radius * 1.5)));
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x0027C28F File Offset: 0x0027A48F
		public override int y1
		{
			get
			{
				return Math.Min(24, Math.Max(0, this.center.Y - this.radius));
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06007035 RID: 28725 RVA: 0x0027C2B0 File Offset: 0x0027A4B0
		public override int x2
		{
			get
			{
				return Math.Max(0, Math.Min(79, this.center.X + (int)((double)this.radius * 1.5)));
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06007036 RID: 28726 RVA: 0x0027C2DD File Offset: 0x0027A4DD
		public override int y2
		{
			get
			{
				return Math.Max(0, Math.Min(24, this.center.Y + this.radius));
			}
		}

		// Token: 0x040033B3 RID: 13235
		protected Location2D center;

		// Token: 0x040033B4 RID: 13236
		protected int radius;
	}
}
