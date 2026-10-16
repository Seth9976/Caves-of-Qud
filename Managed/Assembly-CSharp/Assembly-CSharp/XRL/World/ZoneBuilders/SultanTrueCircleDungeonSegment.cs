using System;
using Genkit;
using UnityEngine;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AEF RID: 2799
	public class SultanTrueCircleDungeonSegment : ISultanDungeonSegment
	{
		// Token: 0x06007037 RID: 28727 RVA: 0x0027C2FE File Offset: 0x0027A4FE
		public SultanTrueCircleDungeonSegment(Location2D pos, int radius)
		{
			this.center = pos;
			this.radius = radius;
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x0027C314 File Offset: 0x0027A514
		public override int width()
		{
			return (int)(2.0 * ((double)this.radius * 1.5)) + 1;
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x0027C334 File Offset: 0x0027A534
		public override int height()
		{
			return 2 * this.radius + 1;
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x0027C340 File Offset: 0x0027A540
		public override bool HasCustomColor(int x, int y)
		{
			return false;
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x0027C343 File Offset: 0x0027A543
		public override Color32 GetCustomColor(int x, int y)
		{
			return ColorOutputMap.BLACK;
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x0027C34A File Offset: 0x0027A54A
		public override bool contains(int x, int y)
		{
			return this.contains(Location2D.Get(x, y));
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x0027C35C File Offset: 0x0027A55C
		public override bool contains(Location2D location)
		{
			double num = (double)this.radius * 1.5;
			double num2 = (double)this.radius * 0.66;
			if (num <= 0.0 || num2 <= 0.0)
			{
				return false;
			}
			Point point = new Point(location.X - this.center.X, (int)((float)(location.Y - this.center.Y) * 0.66f));
			return (double)(point.X * point.X) / (num * num) + (double)(point.Y * point.Y) / (num2 * num2) <= 1.0;
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600703E RID: 28734 RVA: 0x0027C40C File Offset: 0x0027A60C
		public override int x1
		{
			get
			{
				return Math.Min(79, Math.Max(0, this.center.X - (int)((double)this.radius * 1.5)));
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x0027C439 File Offset: 0x0027A639
		public override int y1
		{
			get
			{
				return Math.Min(24, Math.Max(0, this.center.Y - this.radius));
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06007040 RID: 28736 RVA: 0x0027C45A File Offset: 0x0027A65A
		public override int x2
		{
			get
			{
				return Math.Max(0, Math.Min(79, this.center.X + (int)((double)this.radius * 1.5)));
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06007041 RID: 28737 RVA: 0x0027C487 File Offset: 0x0027A687
		public override int y2
		{
			get
			{
				return Math.Max(0, Math.Min(24, this.center.Y + this.radius));
			}
		}

		// Token: 0x040033B5 RID: 13237
		protected Location2D center;

		// Token: 0x040033B6 RID: 13238
		protected int radius;
	}
}
